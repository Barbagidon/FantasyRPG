# 🛡️ FantasyRPG — Wiki Проекта & План Обучения

## 🎯 Финальное видение проекта

Разработка **тактической пошаговой фэнтези RPG с кооперативом на 3 игроков** (*вдохновлено Pillars of Eternity, Baldur's Gate 3, Divinity: Original Sin 2, The Witcher 3*).

### Ключевые требования к результату:
- **Clean Architecture в C#/Unity:** Доменная логика (`Core/`) на 100% отделена от Unity `MonoBehaviour`.
- **Сеть:** Server-Authoritative кооператив на 3 игроков через Unity Netcode for GameObjects (передача рас и ходов через `byte` enums).
- **Оптимизация (Zero-GC Spikes & High FPS):** GC-friendly код (значимые типы `struct`, предварительная аллокация памяти, отсутствие `new` и спама кучи в игровом цикле).
- **UI:** Четкое разделение по паттерну MVP (Model-View-Presenter).

---

## 🧝‍♂️ Концепция рас и кооперативной синергии (Races & Co-op Synergies)

В игре представлено 4 расы, рассчитанные на синергию в отряде из 3 игроков:

1. **👨‍🦯 Человек (Human):** *Тактик и Лидер.* 
   - Бонусы: +1 к `MaxActionPoints`, пассивка «Тактический авангард» (+10% к инициативе группы).
2. **🧝‍♂️ Эльф (Elf):** *Мастер Инициативы.*
   - Бонусы: Высокая `Speed` (ходит первым) и `CritChance`, способность «Жертвенная кровь» (-HP за +1 AP).
3. **🧔 Гном / Дворф (Dwarf):** *Непреклонный Танк.*
   - Бонусы: Высокие `MaxHealth` и `BaseDefense`, пассивка «Каменная опека» (-15% урона по соседним союзникам).
4. **🧟 Нежить / Орк (Undead / Orc):** *Берсерк и Мастер темной магии.*
   - Бонусы: Увеличение `BaseAttack` при падении `CurrentHealth`, иммунитет/исцеление от яда.

---

## 🗺️ План обучения и дорожная карта (Roadmap)

```text
[Сессия 1: Core Domain] ──> [Сессия 2: Combat FSM] ──> [Сессия 3: Quests & Event Bus] ──> [Сессия 4: Unity UI (MVP)] ──> [Сессия 5: 3-Player Netcode]
```

### 🔹 Сессия 1: Доменное ядро (Items & Character Stats) — 100% ЗАВЕРШЕНО 🎉
- [x] Определение типов предметов (`WeaponType`, `ArmorType`)
- [x] Классы предметов (`Weapon`, `Armor`)
- [x] Структура характеристик героя (`HeroStats` — HP, AP, Speed, Attack, Defense, CritChance, CritMultiplier)
- [x] Доменный класс героя (`Hero`)
- [x] Калькулятор урона (`DamageCalculator`)

### 🔹 Сессия 2: Пошаговая боевая система (Combat Engine) — 100% ЗАВЕРШЕНО 🎉
- [x] Интерфейс состояний боя ([ICombatState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatState.cs))
- [x] Дирижер состояний ([CombatStateMachine.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/CombatStateMachine.cs))
- [x] Состояние инициализации ([InitState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/InitState.cs))
- [x] Состояние хода игрока и списание AP ([PlayerTurnState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/PlayerTurnState.cs))
- [x] Состояние хода ИИ врага ([EnemyTurnState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/EnemyTurnState.cs))
- [x] Состояния окончания боя ([VictoryState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/VictoryState.cs), [DefeatState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/DefeatState.cs))
- [x] Пошаговый боевой движок ([TurnBasedCombatEngine.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/TurnBasedCombatEngine.cs))

### 🔹 Сессия 3: Расовая подсистема, Квесты и Event Bus (Quests & Event Bus) — СЛЕДУЮЩАЯ 🚀
- [ ] Расовая подсистема (`RaceType.cs`, `RaceBonus.cs`)
- [ ] Event Bus (Событийный автобус: `OnEnemyKilled`, `OnItemCollected`)
- [ ] Движок квестов и целей (`Quest`, `Objective`)
- [ ] Инвентарь и экипировка предметов (`EquipmentSystem`)

### 🔹 Сессия 4: Презентационный слой и UI (Unity Presentation & UI)
- [ ] MVP архитектура для игрового интерфейса (Model-View-Presenter)
- [ ] Отображение состояния боевки, здоровья и инвентаря
- [ ] Визуализация ходов и анимации

### 🔹 Сессия 5: Сетевой кооператив (3-Player Co-op Networking)
- [ ] Настройка Unity Netcode for GameObjects
- [ ] Server-Authoritative валидация ходов
- [ ] Синхронизация состояний игроков и FSM через RPC / NetworkVariables

---

## 📅 Ежедневный журнал прогресса (Daily Progress Log)

### 📆 2026-07-28
- **Завершено 100% Сессии 2 (Combat Engine):**
  - [EnemyTurnState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/EnemyTurnState.cs) — автономный ход ИИ врага.
  - [VictoryState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/VictoryState.cs) & [DefeatState.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/DefeatState.cs) — состояния финала боя.
  - [TurnBasedCombatEngine.cs](file:///c:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/TurnBasedCombatEngine.cs) — главный пошаговый боевой движок.
- **Проверка:** Компиляция прошла с **0 Предупреждений и 0 Ошибок**!
- **Текущий статус:** Сессия 2 успешно завершена!
- **Следующий шаг:** Переход к Сессии 3 (Расы `RaceType` / `RaceBonus`, Event Bus и Квесты).
