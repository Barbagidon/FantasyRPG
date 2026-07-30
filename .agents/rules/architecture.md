# 🏗️ C# & Unity Architecture Contract (Network-Ready & Production Standards)

## Architectural Blueprint
The project follows **Clean Architecture** tailored for Unity game development:

```text
Assets/Scripts/
  ├── Core/               # Pure C# Domain Layer (Zero Unity MonoBehaviour dependencies)
  │   ├── Items/          # Weapon, Armor, Enums, Inventories
  │   ├── Stats/          # HeroStats, RaceBonus, Modifiers, DamageCalculator
  │   ├── Events/         # Type-Safe Zero-GC Event Bus
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
   - **Auto-Properties over Manual Backing Fields**: Always default to auto-properties `{ get; private set; }` for publicly readable attributes instead of creating redundant `private readonly` fields paired with expression getters `=> _field`.
   - **Memory Efficiency (GC Optimization)**: Use `struct` for small, immutable data containers (e.g., `HeroStats`) to avoid heap allocations and Garbage Collector spikes during combat.
   - **Type Safety**: Use explicit C# types (`int`, `float`, `string`, `enum`, `struct`, `class`). Avoid untyped data containers.
   - **FSM Orchestrator Pattern**: `TurnBasedCombatEngine` acts as the central FSM orchestrator. Individual `ICombatState` classes should only receive parameters in their constructor that are actually used in `Enter()` or `Exit()`.

## Advanced Architectural Design Patterns

4. **Type-Safe & Allocation-Free Event Bus (`FantasyRPG.Core.Events`)**:
   - Zero-GC event bus using generic event types (`IEvent`).
   - Decouples domain state changes (`StatChangedEvent`, `EnemyKilledEvent`, `TurnEndedEvent`) from UI, Quest Engine, and Sound Systems without string keys or boxing.

5. **Command Pattern for Combat & Actions (`FantasyRPG.Core.Combat.Commands`)**:
   - Encapsulate all combat actions (`AttackCommand`, `UseAbilityCommand`, `EquipCommand`) into command objects implementing `ICombatCommand` (`Execute()`, `Validate()`, `Undo()`).
   - Enables action queuing, action validation, undo capability, and server-authoritative RPC transmission.

6. **3-Player Server-Authoritative Netcode (`FantasyRPG.Network`)**:
   - **Single Source of Truth:** Server/Host validates all actions before state mutation.
   - **State vs Events:** Use `NetworkVariable<T>` for persistent state (HP, `CurrentTurnPlayerId`) and RPCs (`ServerRpc`/`ClientRpc`) for discrete events and animation triggers.
   - **Byte-Sized Enums, Built-In Serialization:** Custom enums (`RaceType`, `WeaponType`, `TurnState`) MUST be `: byte`. Rely on NGO's built-in serialization for `NetworkVariable<T>`/RPC parameters — for a 3-player turn-based game this is sufficient. Manual `FastBufferWriter`/`FastBufferReader` serialization is reserved for cases where profiling proves it necessary, not a default requirement.

7. **MVP (Model-View-Presenter) with Passive View for UI (`FantasyRPG.UI`)**:
   - `View` components (`MonoBehaviour` / UI Toolkit) contain zero business logic.
   - `Presenter` classes subscribe to the `Event Bus` and update `View` elements reactively, with zero polling in `Update()`.
