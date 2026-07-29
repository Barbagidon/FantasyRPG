# 🗺️ Дорожная Карта Обучения и Прогресс Разработки

## 🚀 Генеральный План Сессий

```text
[Сессия 1: Core Domain] ──> [Сессия 2: Combat FSM] ──> [Сессия 3: Race & Commands] ──> [Сессия 4: Unity UI (MVP)] ──> [Сессия 5: Netcode Co-op]
```

---

## 🔹 Сессия 1: Доменное ядро (Items & Character Stats) — 100% ЗАВЕРШЕНО 🎉
- [x] Определение типов предметов ([WeaponType.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/WeaponType.cs), [ArmorType.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/ArmorType.cs))
- [x] Классы предметов ([Weapon.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/Weapon.cs), [Armor.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/Armor.cs))
- [x] Структура характеристик героя ([HeroStats.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/HeroStats.cs) — HP, AP, Speed, Attack, Defense, Crit)
- [x] Доменный класс героя ([Hero.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/Hero.cs))
- [x] Калькулятор урона ([DamageCalculator.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/DamageCalculator.cs))

---

## 🔹 Сессия 2: Пошаговая боевая система (Combat Engine) — 100% ЗАВЕРШЕНО 🎉
- [x] Интерфейс состояний боя ([ICombatState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatState.cs))
- [x] Дирижер состояний ([CombatStateMachine.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/CombatStateMachine.cs))
- [x] Состояние инициализации ([InitState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/InitState.cs))
- [x] Состояние хода игрока ([PlayerTurnState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/PlayerTurnState.cs))
- [x] Состояние хода ИИ врага ([EnemyTurnState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/EnemyTurnState.cs))
- [x] Состояния окончания боя ([VictoryState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/VictoryState.cs), [DefeatState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/DefeatState.cs))
- [x] Главный боевой движок ([TurnBasedCombatEngine.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/TurnBasedCombatEngine.cs))

---

## 🔹 Сессия 3: Расовая подсистема, Команды и Квесты (Race Subsystem & Commands) — В ПРОЦЕССЕ 🚀
- [x] **Паттерн Command для боевых действий** ([ICombatCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatCommand.cs), [AttackCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/AttackCommand.cs)) 🎉
- [ ] Расовая подсистема (`RaceType.cs`, `RaceBonus.cs`, пассивные и активные абилки рас)
- [ ] Движок квестов и целей (`Quest`, `Objective`, `QuestLog`)
- [ ] Инвентарь и экипировка предметов (`EquipmentSystem`)

---

## 🔹 Сессия 4: Презентационный слой и UI (Unity Presentation & UI)
- [ ] MVP архитектура для игрового интерфейса (Model-View-Presenter)
- [ ] Отображение состояния боевки, здоровья и инвентаря
- [ ] Визуализация ходов и анимации

---

## 🔹 Сессия 5: Сетевой кооператив (3-Player Co-op Networking)
- [ ] Настройка Unity Netcode for GameObjects
- [ ] Server-Authoritative валидация ходов
- [ ] Синхронизация состояний игроков и FSM через RPC / NetworkVariables

---

## 📅 Журнал прогресса (Progress Log)

### 📆 2026-07-28
- Разделена вики-документация на специализированные модульные файлы в папке `docs/`.
- Добавлены графические диаграммы на Mermaid в разделы Архитектуры, Модели Домена и Боевой системы.
- Реализован паттерн Command ([ICombatCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatCommand.cs) & [AttackCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/AttackCommand.cs)).

---

## 🔗 Сопутствующие разделы:
- 🏛️ [Архитектура Проекта](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/architecture.md)
- 📊 [Модель Домена](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/domain_model.md)
- 🔄 [Боевая Система](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/combat_system.md)
- 🧝‍♂️ [Расы и Синергия](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/races_and_synergies.md)
