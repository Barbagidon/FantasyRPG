# 🔍 Unity C# & Netcode Code Review Checklist (Zero-GC Standards)

This document specifies the strict code review rules for Unity C#, Clean Architecture, Netcode for GameObjects (NGO), Zero-GC performance, and static analysis hygiene.

---

## 0. Build Target & Project Integrity (Валидность сборки)
- **Real Compilation Scope:** Verify that `.csproj` actually includes target source code from `Assets/Scripts/` (e.g. `<Compile Include="Assets\Scripts\**\*.cs" />`) so that `dotnet build` validates actual game code rather than skipping it.
- **Call-Site Signature Match:** When modifying any constructor or method signature, ALWAYS cross-check all instantiation and invocation sites (Call Sites) across the entire codebase to match exact argument counts and types.

---

## 1. Clean Architecture & Layer Decoupled Rule
- **Domain Independence:** `FantasyRPG.Core.*` classes MUST NEVER inherit from `MonoBehaviour` or `NetworkBehaviour`.
- **Dependency Inversion:** Outer layers (Unity presentation/UI) depend on inner domain abstractions (`Core`), NEVER vice versa.
- **Pure Entities:** Entities must contain business logic and properties with `{ get; private set; }`.

---

## 2. Zero-GC (Garbage Collector Optimization) Rule
- **No `new` Allocations in Hot Loops:** Zero allocations inside `Update()`, `FixedUpdate()`, or turn-based combat iterations.
- **Use `struct` for Small Value Containers:** Stat snapshots, damage payloads, and math containers MUST be `readonly struct`.
- **Non-Allocating Unity APIs:** Use non-allocating variants (`RaycastNonAlloc`, `OverlapSphereNonAlloc`) instead of array-allocating queries.
- **Object Pooling:** All frequently spawned entities (VFX, projectiles, numbers) MUST use pre-allocated Object Pools instead of `Instantiate`/`Destroy`.
- **No Boxing/Unboxing:** Avoid `object` parameters or untyped interfaces. Use generic constraints (`T where T : struct`).
- **No Inline Lambdas in Sort/Loops:** Disallow anonymous lambda expressions `(a, b) => ...` inside `List.Sort()` or loop methods. Mandate `static readonly Comparison<T>` or `IComparer<T>` to guarantee zero delegate allocations.

---

## 3. Netcode for GameObjects (NGO) Optimization Rule
- **Bit-Packed Serialization:** Custom enums (e.g. `RaceType`, `WeaponType`, `TurnState`) MUST explicitly specify `: byte` (`enum RaceType : byte`) and be serialized via `FastBufferWriter` / `FastBufferReader`.
- **Server-Authoritative Validation:** All state-changing actions (spending AP, dealing damage, equipping items) MUST be validated on the Server/Host before mutating domain state.
- **Throttled Network Variables:** Use minimal `NetworkVariable<T>` properties; prefer RPCs for one-off discrete actions (attacks, turn changes).

---

## 4. Roslyn, Static Analysis & Completeness Hygiene
- **Zero Unused Parameters (IDE0060):** All constructor/method parameters MUST be referenced.
- **Zero Unread Private Fields/Methods (IDE0051/IDE0052):** Every private field and private method MUST be called and read in class logic.
- **Zero Unused Usings (IDE0005):** Remove unreferenced `using` directives.
- **FSM & Lifecycle Completeness:** Verify that state machines and engines have complete lifecycle loops: initialization, state transitions (e.g. `NextTurn()`), and victory/defeat termination conditions.
- **Deterministic Build Gate:** Every PR/step MUST pass `dotnet build` with 0 Warnings and 0 Errors against the complete codebase.

---

## 5. Mutable Runtime State Separation Rule
- **Never store mutable gameplay values in `readonly struct`:** Properties that change during gameplay (`CurrentHealth`, `CurrentActionPoints`, `CurrentMana`) MUST NOT reside in a `readonly struct`. A `readonly struct` is for immutable snapshots (base stats, damage payloads). Violation makes it physically impossible to apply damage or consume AP.
- **Separation contract:** Define `BaseStats` as a `readonly struct` (immutable: `MaxHealth`, `BaseAttack`, `Speed`) and store mutable runtime values as `private` fields directly on the domain class (e.g., `Hero._currentHealth`), exposed via a `{ get; private set; }` property.
- **Required domain methods:** Any entity that participates in receiving damage MUST expose `ApplyDamage(int amount)`. Any entity that participates in spending AP MUST expose `SpendAP(int cost)`. These methods are mandatory — their absence is a BLOCKER.

---

## 6. Empty Public Method Body Prohibition
- **`public` methods with an empty body `{ }` are FORBIDDEN** if the method name implies domain logic (e.g., `AdvanceTurn`, `Execute`, `Apply`, `Calculate`, `Process`). An empty body on a game-loop method silently breaks the entire cycle with no compiler error.
- **Permitted exceptions:** `Exit()` / `Enter()` stubs in terminal FSM states (`VictoryState`, `DefeatState`) are allowed when explicitly commented: `// No cleanup required for terminal state.`
- **Review gate:** Before approving any PR, scan all `public` and `protected` methods for empty bodies and demand either an implementation or a documented reason.

---

## 7. Property-to-Logic Consistency Rule
- **Every domain property that governs behavior MUST be consumed by domain logic.** If `HeroStats` exposes `CritChance`, the `DamageCalculator` (or equivalent) MUST use it to determine critical hits. A property that exists but is never read by logic is dead code disguised as a feature — it will silently produce wrong game behavior.
- **Review gate:** For each stat/property added to a domain entity, verify there is at least one caller in the domain layer that reads and acts on it.

---

## 8. Explicit `using` Directives Rule
- **All type dependencies MUST be declared via explicit `using` directives** at the top of every file, regardless of whether the Unity/MSBuild configuration currently resolves them without it.
- **Rationale:** Implicit cross-namespace resolution works only as long as all scripts share one assembly (`Assembly-CSharp`). The moment any `asmdef` assembly split occurs (which is a standard Unity optimization), missing `using` statements become immediate compile errors (CS0246). Do not accumulate this technical debt.
- **Review gate:** Every file using a type from a foreign namespace must have the corresponding `using` line — even if the project currently compiles without it.

---

## 9. Core Domain Unit Test Gate
- **Every `public` method in `Core/` that contains branching logic MUST have at least one unit test** covering the happy path. Priority targets: `DamageCalculator.CalculateDamage()`, `TurnBasedCombatEngine.CheckCombatEnd()`, `PlayerTurnState.TrySpendAP()`, `TurnBasedCombatEngine.AdvanceTurn()`.
- **Test location:** `Assets/Tests/Core/` (Unity Test Runner, Edit Mode).
- **Rationale:** Domain logic is pure C# with no Unity scene dependencies — it is trivially testable. Untested domain logic means broken game states that are discovered only at runtime, making debugging in multiplayer sessions extremely costly.
- **Gate:** A session is NOT complete if new domain logic was added without a corresponding test covering its core invariant.
