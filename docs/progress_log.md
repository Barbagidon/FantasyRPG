# 📅 Журнал Прогресса Разработки (Progress Log & Changelog)

Этот документ содержит хронологию сессий, выполненных задач и решений в проекте **FantasyRPG**.

---

## 📆 2026-07-28 — Сессия 2 (100%), Старт Сессии 3, Рефакторинг Вики & Персистентные Квесты

### 🎯 Выполненные Задачи и Дизайнерские Решения:

1. **Концепция Персистентного Мира и Квестов (Kingdom Come: Deliverance Style):**
   - Сформулированы архитектурные требования к спавну NPC: все ключевые NPC спавнятся при старте игры, а не динамически по скрипту при взятии квеста.
   - Спроектирован механизм **Fail-Safe / Альтернативных веток квестов**: при случайном убийстве ключевого NPC до или во время квеста система через `EventBus` и `WorldStateRepository` автоматически переключает квест на альтернативную цель (*«Осмотреть труп»*, *«Доложить о смерти»*).
   - Создан отдельный модуль документации [docs/quest_system.md](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/quest_system.md) с Mermaid-схемой реакции системы на гибель персонажа.

2. **Завершение 100% Сессии 2 (Пошаговый Боевой Движок):**
   - Реализована автономная логика хода ИИ врага в [EnemyTurnState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/EnemyTurnState.cs).
   - Созданы финальные состояния боя [VictoryState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/VictoryState.cs) и [DefeatState.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/DefeatState.cs).
   - Интегрирован главный оркестратор очереди ходов [TurnBasedCombatEngine.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/TurnBasedCombatEngine.cs).

3. **Старт Сессии 3 (Паттерн Command для боевых действий):**
   - Создан интерфейс боевых команд [ICombatCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/ICombatCommand.cs).
   - Реализована базовая команда атаки [AttackCommand.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Combat/AttackCommand.cs) с поддержкой критов и [DamageCalculator.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/DamageCalculator.cs).

4. **Модульный рефакторинг Вики:**
   - Разделена базовая вики на отдельные узкоспециализированные Markdown-документы в папке `docs/` с графикой на Mermaid.

---

## 📆 2026-07-27 — Сессия 1 (Доменное Ядро)

### 🎯 Выполненные Задачи:
- Созданы базовые энумы предметов ([WeaponType.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/WeaponType.cs), [ArmorType.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/ArmorType.cs)).
- Реализованы сущности предмета ([Weapon.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/Weapon.cs), [Armor.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Items/Armor.cs)).
- Разработан класс героя ([Hero.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/Hero.cs)) с методами экипировки и расчетом полученного урона.
- Реализован статический калькулятор урона ([DamageCalculator.cs](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/Assets/Scripts/Core/Stats/DamageCalculator.cs)).

---

## 🔗 Сопутствующие разделы:
- 📜 [Подсистема Квестов](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/quest_system.md)
- 🏛️ [Архитектура Проекта](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/architecture.md)
- 📊 [Модель Домена](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/domain_model.md)
- 🔄 [Боевая Система](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/combat_system.md)
- 🧝‍♂️ [Расы и Синергия](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/races_and_synergies.md)
- 🗺️ [Дорожная Карта](file:///C:/Users/shtil/OneDrive/Desktop/FantasyRPG/docs/roadmap.md)
