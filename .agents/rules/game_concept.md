# ⚔️ FantasyRPG: Game Systems & Design Specifications

> Updated 2026-07-30 after a design-validation session. Scope was deliberately trimmed for a
> solo developer at 10–15 h/week. See `docs/roadmap.md` for the full milestone plan and the
> explicit "cut" list with reasoning. This file describes the **target** design — most of it
> is not implemented yet; check `docs/progress_log.md` for actual status.

## High-Level Concept
- **Genre:** Tactical Turn-Based Fantasy PvE RPG with a linear story campaign (10–12 battles) and up to 3-player co-op.
- **Inspirations:** *Pillars of Eternity*, *Baldur's Gate 3*, *Divinity: Original Sin 2* — for combat feel only, not for full scope (see cuts below).
- **Target Experience:** A finishable, personally-built RPG demonstrating clean architecture, state machines, and networked multiplayer — not a AAA-scope recreation of the references.
- **Structure:** camp/hub (static scene, clickable points) → narrator-box story beat with a choice → battle → repeat. No open world, no walkable hub, no NPC dialogue engine.

## Core Game Subsystems

### 1. Equipment & Items Subsystem (`FantasyRPG.Core.Items`)
- **`WeaponType` (Enum):** `Sword`, `Dagger`, `Bow`, `Staff`, `Axe`.
- **`Weapon` (Class):** Name, Type, BaseDamage.
- **`ArmorType` (Enum):** `Helmet`, `Chestplate`, `Boots`, `Shield`.
- **`Armor` (Class):** Name, Type, Defense.
- **Scope note:** 2–3 equipment slots per hero, granted by the story between missions. No full inventory, no loot drops, no economy.

### 2. Character & Stats Subsystem (`FantasyRPG.Core.Stats`)
- **`HeroStats` (readonly struct):** Immutable **base** stats only — `MaxHealth`, `StartingActionPoints`, `ActionPointsPerTurn`, `MaxActionPoints`, `BaseAttack`, `BaseDefense`, `Initiative`, `Movement`, `CritChance`, `CritMultiplier`. Mutable runtime values (`CurrentHealth`, `CurrentActionPoints`) live on `Hero` as private fields with `{ get; private set; }`, never inside the struct — see `code_review_rules.md` §5. The three AP stats and `Movement` are the target design accepted in [ADR-0002](../../docs/decisions/0002-action-points-and-movement-economy.md) and are **not implemented yet** — the code still has the old `MaxActionPoints` ("AP per turn") and `MoveSpeed`.
- **`Initiative` vs `Movement`:** two separate stats. `Initiative` determines turn order; `Movement` modifies the AP cost of **each movement edge** via `ceil(baseStepCost * 100 / Movement)`, neutral at `100`. The old single `Speed` field conflated turn order with movement — split during Веха 0. **`MoveSpeed` renamed to `Movement` with changed semantics by ADR-0002:** the earlier description "how many grid cells one AP of movement covers" is superseded — one AP no longer buys whole cells, a single edge costs 10/14 AP scaled by `Movement`.
- **AP economy:** one shared currency for movement, attacks, abilities and items (no separate Movement Points); unspent AP carries over between turns capped by `MaxActionPoints`; a turn ends only on an explicit `EndTurn`, never automatically at 0 AP. See ADR-0002 for the full contract.
- **`RaceType` (Enum) & `RaceBonus` (Struct):** `Human`, `Elf`, `Dwarf`, `UndeadOrc`. Exactly **3 fixed, hand-written heroes** use 3 of these races; the 4th race appears among enemies or story characters. No player-created characters.
- **`DamageCalculator` (Class):** Pure static/domain methods. First version: physical damage + one magic damage type. Full 5-element damage/resistance matrix is deferred past the first playable campaign (see roadmap cuts).

### 3. Turn-Based Combat Subsystem (`FantasyRPG.Core.Combat`)
- **Grid:** integer `(x, y)` coordinates, `Assets/Scripts/Core`. 8-directional movement, orthogonal step cheaper than diagonal (e.g. 10 vs 14), no cutting a diagonal through a blocked corner — a diagonal needs **both** flanking orthogonal cells passable, per [ADR-0003](../../docs/decisions/0003-movement-legality-occupancy-and-diagonal-corners.md).
- **Occupancy:** at most one living unit per cell. You may path *through* an ally but not stop on their cell; an enemy's cell blocks movement entirely (body-blocking). Corpses don't block. Consequence worth knowing while designing encounters: an orthogonally adjacent enemy geometrically denies three of your eight directions. See ADR-0003.
- **AP System:** Action Points spent on movement and abilities via `ICombatCommand`. No verticality/High Ground, no free (NavMesh) movement — both cut deliberately, see roadmap.
- **Line of Sight:** Bresenham-style, blocks ranged attacks and reveals cover bonus. Implemented as pure `Core` logic, unit-testable.
- **Finite State Machine (FSM):** States for `InitState` (turn order sorted by `Initiative`, not `Speed`), `PlayerTurnState`, `EnemyTurnState`, `VictoryState`, `DefeatState`. Strict round-robin, no simultaneous actions, no `Delay Turn` mechanic (deliberately cut — adds FSM complexity the strict-order/one-hero-per-player model is meant to avoid).
- **Co-op turn ownership:** each unit has an `OwnerId`. In solo play all heroes belong to the host. In co-op, players distribute the 3 heroes among themselves (not strictly 1:1 — with 2 players, one holds two heroes).

### 4. Story & Campaign (`FantasyRPG.Core.Campaign`)
- **No quest engine, no NPC dialogue nodes, no persistent open world.** Story beats are narrator boxes: text + 2–3 choices, backed by data (not hardcoded), with flags that affect later text — not branching mission structure.
- Linear sequence of 10–12 missions ("the road" framing). Camp/hub between missions is a static scene with clickable points, not a walkable space.
- **`Quest`/`QuestLog` classes from the old design are cut.** If replayed later, campaign progression is tracked via simple flags + current mission index, not a quest object graph.

### 5. Up-to-3-Player Co-op Networking (`FantasyRPG.Network`)
- **Server-Authoritative State:** Host/Server validates all player turn actions, including crit rolls (never trust a client roll).
- **Commands as data:** `ICombatCommand` implementations carry unit ID, ability ID, target cell/ID — never a direct reference to `Hero`, `GameObject`, or `MonoBehaviour`.
- **Synchronization:** Turn state and race enums (`byte`) synced via NGO's built-in serialization (`NetworkVariable<T>`/RPC) — see `code_review_rules.md` §3 for why manual `FastBufferWriter` is not a default requirement here.
- **Explicitly out of scope:** matchmaking, public lobby list, anti-cheat, host migration, reconnect mid-battle.
