# ⚔️ FantasyRPG: Game Systems & Design Specifications

## High-Level Concept
- **Genre:** Tactical Turn-Based Fantasy RPG with 3-Player Co-op.
- **Inspirations:** *Pillars of Eternity*, *Baldur's Gate 3*, *Divinity: Original Sin 2*, *The Witcher 3*.
- **Target Experience:** Production-grade, resume-worthy RPG codebase demonstrating clean architecture, state machines, zero-GC performance, and networked multiplayer.

## Core Game Subsystems

### 1. Equipment & Items Subsystem (`FantasyRPG.Core.Items`)
- **`WeaponType` (Enum):** `Sword`, `Dagger`, `Bow`, `Staff`, `Axe`.
- **`Weapon` (Class):** Name, Type, BaseDamage.
- **`ArmorType` (Enum):** `Helmet`, `Chestplate`, `Boots`, `Shield`.
- **`Armor` (Class):** Name, Type, Defense.

### 2. Character & Stats Subsystem (`FantasyRPG.Core.Stats`)
- **`HeroStats` (Struct):** Immutable value container holding `MaxHealth`, `CurrentHealth`, `MaxActionPoints`, `BaseAttack`, `BaseDefense`, `Speed` (Turn initiative), `CritChance`, `CritMultiplier`.
- **`RaceType` (Enum) & `RaceBonus` (Struct):** `Human`, `Elf`, `Dwarf`, `UndeadOrc`. Race stat modifiers and 3-Player co-op team synergies.
- **`DamageCalculator` (Class):** Pure static/domain methods to calculate physical, magical, and critical damage taking armor, critical modifiers, and elemental attributes into account.

### 3. Turn-Based Combat Subsystem (`FantasyRPG.Core.Combat`)
- **Divinity-Style AP System:** Action Points (AP) turn management and energy costs for attacks, movement, and abilities.
- **Finite State Machine (FSM):** States for `InitState` (Turn initiative sorting based on `Speed`), `PlayerTurnState`, `EnemyTurnState`, `VictoryState`, `DefeatState`.

### 4. Quest & Rewards Subsystem (`FantasyRPG.Core.Quests`)
- **`Quest` & `Objective`:** Kill objectives, fetch/item objectives, dialogue objectives.
- **Event-Driven Progress:** Event Bus listening to game events (`OnEnemyKilled`, `OnItemCollected`) to update active quests asynchronously.

### 5. 3-Player Co-op Networking (`FantasyRPG.Network`)
- **Server-Authoritative State:** Host/Server validates all player turn actions.
- **Synchronization:** Turn state, race enums (`byte`), and health synced across 3 clients using Unity Netcode for GameObjects.
