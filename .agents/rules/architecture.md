# 🏗️ C# & Unity Architecture Contract (Network-Ready & Production Standards)

## Architectural Blueprint
The project follows **Clean Architecture** tailored for Unity game development:

```text
Assets/Scripts/
  ├── Core/               # Pure C# Domain Layer (Zero Unity MonoBehaviour dependencies)
  │   ├── Items/          # Weapon, Armor, Enums, Inventories
  │   ├── Stats/          # HeroStats, Modifiers, ModifiersCalculator
  │   └── Combat/         # Turn-Based FSM, DamageCalculator, Command Pattern
  ├── Network/            # Unity Netcode for GameObjects (3-Player Co-op synchronization)
  ├── Presentation/       # Unity MonoBehaviours, Animations, VFX, Audio
  └── UI/                 # MVP (Model-View-Presenter) UI architecture
```

## Mandatory Engineering Rules

1. **Namespace & Folder Hierarchy 1-to-1 Mapping**:
   - Every file's `namespace` MUST strictly mirror its folder path under `Assets/Scripts/`.
   - Example: `Assets/Scripts/Core/Items/Weapon.cs` -> `namespace FantasyRPG.Core.Items`.

2. **Network-Ready Requirement (3-Player Co-op)**:
   - All domain logic (`Core/`) MUST be 100% decoupled from Unity scene visual components (`MonoBehaviour`).
   - Domain models must be deterministic and pure so they can be processed on a dedicated server / host or synchronized via Network Variables / RPCs.

3. **C# Code Standards & Encapsulation**:
   - **Properties**: Use `{ get; private set; }` for domain entity attributes to enforce strict read-only access from external classes.
   - **Memory Efficiency (GC Optimization)**: Use `struct` for small, immutable data containers (e.g., `HeroStats`) to avoid heap allocations and Garbage Collector spikes during combat.
   - **Type Safety**: Use explicit C# types (`int`, `float`, `string`, `enum`, `struct`, `class`). Avoid untyped data containers.
   - **FSM Orchestrator Pattern**: `TurnBasedCombatEngine` acts as the central FSM orchestrator. Individual `ICombatState` classes should only receive parameters in their constructor that are actually used in `Enter()` or `Exit()`. Avoid passing unused `CombatStateMachine` references to state classes that do not trigger transitions internally.
