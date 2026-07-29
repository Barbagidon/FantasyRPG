# 🗺️ Дорожная Карта Обучения и Прогресс Разработки

## 🚀 Генеральный План Сессий

```text
[Сессия 1: Core Domain] ──> [Сессия 2: Combat FSM] ──> [Сессия 2.5: Unit Tests] ──> [Сессия 3: AI, Grid] ──> [Сессия 4: Unity UI (MVP)] ──> [Сессия 5: Netcode Co-op] ──> [Сессия 6: Save/Load]
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
- [x] Каркас состояния хода врага ([EnemyTurnState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/EnemyTurnState.cs)) — выдача AP (мозг ИИ будет в Сессии 3).
- [x] Состояния окончания боя ([VictoryState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/VictoryState.cs), [DefeatState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/DefeatState.cs))
- [x] Главный боевой движок ([TurnBasedCombatEngine.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/TurnBasedCombatEngine.cs))

---

## 🔹 Сессия 2.5: Тестирование Фундамента (TDD & Unit Tests) — СЛЕДУЮЩАЯ 🚀
- [ ] Настройка папки `Tests/` и Unity Test Framework.
- [ ] Написание тестов для `DamageCalculator` (защита, расчет крита).
- [ ] Написание тестов для `Hero` (корректное снятие ХП, проверка смерти).
- [ ] Написание тестов для `CombatStateMachine` (проверка логики смены ходов).

---

## 🔹 Сессия 3: Интеллект Врагов, Пространство и Расы (AI, Grid, Races)
- [x] **Паттерн Command для боевых действий** ([ICombatCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatCommand.cs), [AttackCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/AttackCommand.cs)) 🎉
- [ ] **Пространство (Divinity-style):** Система координат, `MoveCommand`, препятствия, Линия видимости (LoS) и **Вертикальность (High Ground)**.
- [ ] **Умный ИИ Врагов (Utility AI):** Оценка действий врагом в `EnemyTurnState` (выбор цели, перемещение, атака).
- [ ] **Реакции и Свободные Атаки:** Атаки по возможности (Attacks of Opportunity).
- [ ] **Раздельная Броня (Dual Armor):** Физическая и Магическая броня для защиты от контроля (CC).
- [ ] **Движок Навыков и Состояний (Skills & Status Effects):** Баффы, дебаффы (яд, горение), перенос AP, Кулдауны и Ограничения (Charges).
- [ ] Расовая подсистема (`RaceType.cs`, `RaceBonus.cs`, пассивные и активные абилки рас)
- [ ] Движок квестов и прогрессии (`Quest`, `QuestLog`, Система Опыта и Leveling)
- [ ] **Система Диалогов (Dialogue System):** Узловые диалоги для взятия квестов.
- [ ] Инвентарь, экипировка и система Лута (Дроп с врагов, `EquipmentSystem`)
- [ ] **Интерактивное Окружение:** Интерфейс `IInteractable` (сундуки, бочки, двери).

---

## 🔹 Сессия 4: Презентационный слой и UI (Unity Presentation & UI)
- [ ] MVP архитектура для игрового интерфейса (Model-View-Presenter)
- [ ] **Очередь ходов (Initiative Tracker):** Визуализация очереди и механика "Delay Turn"
- [ ] Отображение состояния боевки, здоровья и инвентаря
- [ ] Визуализация ходов и анимации

---

## 🔹 Сессия 5: Сетевой кооператив (3-Player Co-op Networking)
- [ ] Настройка Unity Netcode for GameObjects
- [ ] Server-Authoritative валидация ходов
- [ ] Синхронизация состояний игроков и FSM через RPC / NetworkVariables

---

## 🔹 Сессия 6: Сохранение и Загрузка (Save/Load & Persistence)
- [ ] Сериализация состояния мира (World State) и инвентаря
- [ ] Сохранение прогресса квестов (Event Bus & WorldStateRepository)
- [ ] Синхронизация сохранений в кооперативе (Host-based saves)

---

## 🤔 Идеи на будущее (Mechanics for Discussion)
*Механики, усложняющие разработку, но критичные для жанра (взяты на обсуждение):*
- Стихийные поверхности (Огонь, Вода, Яд) и их взаимодействия.

---

## 📅 Журнал прогресса (Progress Log)

### 📆 2026-07-29
- Утверждена философия тестирования (TDD для Core). В план добавлена "Сессия 2.5" для покрытия существующего кода тестами перед переходом к сложным механикам.
- Третья волна аудита (Web Research): добавлены Раздельная броня (Physical/Magic Armor), Атаки по возможности и Вертикальность (High Ground) в Сессию 3.
- Вторая волна аудита: официально добавлены Система Диалогов, Интерактивное окружение (`IInteractable`), Кулдауны навыков (Сессия 3), а также Шкала инициативы с механикой пропуска хода (Сессия 4).
- Аудит кодовой базы: выявлена и исправлена неточность в планах (ИИ был отмечен как готовый, но это был только стейт-каркас).
- В Сессию 3 добавлены задачи по разработке Utility AI (умные враги) и системы координат с `MoveCommand` в стиле Divinity.
- Утвержден план писать ИИ с нуля без сторонних плагинов ради соблюдения архитектурной чистоты (Zero-GC).

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
