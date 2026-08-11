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
- **Silent boxing trap — an unqualified `Equals(a, b)` inside a `static` member:** in a `static` method or operator there is no implicit `this`, so instance methods (`Equals(GridPosition)`, `Equals(object)`) are not candidates at all — the compiler resolves the call to the inherited **static** `object.Equals(object, object)`, which boxes both `struct` operands on every call. The result is *correct* and no test can catch it; only review can. Always name the receiver: `left.Equals(right)`, never `Equals(left, right)`. Found 2026-08-07 in `GridPosition.operator ==`, which is on the Dijkstra hot path — the exact place where `readonly struct` exists to avoid allocations. Boxing in a `throw` path (e.g. passing a `struct` as `actualValue` of an argument exception) is acceptable by contrast: it runs only on failure, where allocating the exception itself dwarfs it.
- **No Inline Lambdas in Sort/Loops:** Disallow anonymous lambda expressions `(a, b) => ...` inside `List.Sort()` or loop methods. Mandate `static readonly Comparison<T>` or `IComparer<T>` to guarantee zero delegate allocations.

---

## 3. Netcode for GameObjects (NGO) Optimization Rule
- **Byte-Sized Enums:** Custom enums (e.g. `RaceType`, `WeaponType`, `TurnState`) MUST explicitly specify `: byte` (`enum RaceType : byte`) — this is free and keeps default NGO serialization compact.
- **Prefer Built-In Serialization:** For a 3-player turn-based game, `NetworkVariable<T>` and RPC parameters with NGO's built-in serialization are sufficient for all current data (positions, HP, AP, turn state). Do NOT hand-roll `FastBufferWriter`/`FastBufferReader` serialization unless a profiler run or a specific non-primitive structure proves the built-in path is actually a bottleneck. Manual bit-packing for a 3-player co-op game is premature optimization until proven otherwise.
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
- **Read the test COUNT, not just "green" — a test without `[Test]`/`[TestCase]` silently never runs:** NUnit discovers tests by attribute. A test method that lost (or never got) its attribute is just an ordinary public method: the build stays green, `dotnet test` reports success, the method reads as coverage in review, and it is never executed. Found 2026-08-07 — `ToString_DifferentCoordinates_ReturnsCoordinatesInXYOrder` was written, looked complete in the file, and the run still reported `48`, not `49`. **After adding N tests, verify the reported total grew by exactly N** (`пройдено 49 ... всего 49`) before treating them as coverage, and quote the number when claiming coverage in `docs/progress_log.md` (see `mentor_rules.md` §12). The same check catches the mirror-image mistake: a `[TestCase]`-parameterized method whose assertion hardcodes one case's expected value, which breaks the moment a second case is added.
- **Assertion API — plain `Assert`, not `ClassicAssert`:** Unity Test Framework bundles its own NUnit integration (`com.unity.ext.nunit`) based on **NUnit 3.5**, confirmed via web search 2026-08-03 (Unity has not upgraded past 3.5 as of this writing). Classic assertions (`Assert.AreEqual`, `Assert.IsTrue`, `Assert.IsNull`, `Assert.IsInstanceOf<T>`) are part of the public `Assert` class throughout the NUnit 3.x line — they were only moved to `NUnit.Framework.Legacy.ClassicAssert` in NUnit 4.0. Since `Assets/Tests/Core/**/*.cs` is compiled by both Unity (NUnit 3.5) and `Tests.Core/FantasyRPG.Core.Tests.csproj` (`dotnet build`), the NuGet package there is pinned to `NUnit 3.14.0` (not 4.x) specifically so `Assert.AreEqual(...)` etc. compile identically in both places — do not add `using NUnit.Framework.Legacy;` / `ClassicAssert` or bump the `NUnit` package past the 3.x line without re-verifying Unity's bundled version first.
