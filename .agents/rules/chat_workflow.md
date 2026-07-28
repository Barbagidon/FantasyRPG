# 🔄 Multi-Chat Workflow & Context Management Protocol

## Core Philosophy
To prevent "context rot" and optimize token consumption:
1. Project development is split across focused, modular chat threads (each thread targets a single domain subsystem).
2. Deep research, architectural trade-offs, and complex reasoning conclusions are **immediately persisted into `.agents/rules/` and `project_wiki.md`** so future sessions consume ready-to-use knowledge without spending tokens re-reasoning from scratch.

## Session Lifecycle & Handoff Protocol

### 1. Session Initialization
When starting a new chat thread in this workspace:
- Read `.agents/rules/mentor_rules.md` for interaction/mentorship rules.
- Read `.agents/rules/architecture.md` for C# & Unity engineering rules.
- Read `.agents/rules/game_concept.md` for game systems specifications.
- Read `project_wiki.md` to identify current progress and active tasks.

### 2. Session Scoping & Subsystem Isolation
Keep each chat session strictly scoped to its assigned subsystem:
- **Session 1 (Domain Core):** Items (`Weapon`, `Armor`) and Character Stats (`HeroStats`, `Hero`).
- **Session 2 (Domain Combat):** Turn-Based FSM, Action Points, DamageCalculator.
- **Session 3 (Quests & Inventory):** Quest Engine, Event Bus, EquipmentSystem.
- **Session 4 (Unity Presentation & UI):** MVP Presenters, Views, Scene GameObjects.
- **Session 5 (Networking):** Unity Netcode for GameObjects (3-Player Co-op host & client RPCs).

### 3. Session Closure & Handoff Gate
Before concluding any chat thread or instructing the user to open a new chat:
1. **Deterministic Build Gate:** Execute `dotnet build Assembly-CSharp.csproj` ONLY when C# source code (`.cs`), project files (`.csproj`), or dependencies were modified. Do NOT run compiler builds for pure markdown (`.md`) or documentation updates.
2. **Update Wiki:** Update `project_wiki.md` with completed tasks, newly created files, and updated roadmap status.
3. **Handoff Summary:** Provide the user with a concise summary of accomplishments and the exact starting task for the next chat thread.
