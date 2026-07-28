# 🔍 Unity C# & Netcode Code Review Checklist (Zero-GC Standards)

This document specifies the strict code review rules for Unity C#, Clean Architecture, Netcode for GameObjects (NGO), and Zero-GC performance.

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

---

## 3. Netcode for GameObjects (NGO) Optimization Rule
- **Bit-Packed Serialization:** Custom enums (e.g. `RaceType`, `WeaponType`, `TurnState`) MUST be serialized as `byte` via `FastBufferWriter` / `FastBufferReader` or byte-backed enums (`enum RaceType : byte`).
- **Server-Authoritative Validation:** All state-changing actions (spending AP, dealing damage, equipping items) MUST be validated on the Server/Host before mutating domain state.
- **Throttled Network Variables:** Use minimal `NetworkVariable<T>` properties; prefer RPCs for one-off discrete actions (attacks, turn changes).

---

## 4. Roslyn & Static Analysis Code Hygiene
- **Zero Unused Parameters (IDE0060):** All constructor/method parameters MUST be referenced.
- **Zero Unread Private Fields (IDE0052):** Every private field MUST be read in class logic.
- **Zero Unused Usings (IDE0005):** Remove unreferenced `using` directives.
- **Deterministic Build Gate:** Every PR/step MUST pass `dotnet build` with 0 Warnings and 0 Errors.
