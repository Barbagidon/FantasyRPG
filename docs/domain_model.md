# 📊 Доменная Модель персонажей и предметов

## 🧬 Описание доменного слоя (Domain Core)

Доменный слой отвечает за сущности героев, их характеристики, предметы экипировки и формулы расчета физического/магического урона.

---

## 📊 Иерархия классов (Domain Class Diagram)

> Целевой дизайн после Вехи 0 (см. `docs/roadmap.md`). На момент последнего аудита (2026-07-29)
> `CurrentHealth` физически лежит внутри `readonly struct HeroStats`, что делает `TakeDamage`
> дорогим (пересоздание структуры целиком) и концептуально неверным — здоровье меняется и
> имеет идентичность, а не является неизменяемым значением. Ниже — целевое состояние, не текущее.

```mermaid
classDiagram
    direction TB
    class DamageCalculator {
        <<static>>
        +CalculateDamage(Hero attacker, Hero defender, bool isCritical) int
    }

    class Hero {
        +string Name
        +HeroStats BaseStats
        +int CurrentHealth
        +int CurrentActionPoints
        +Weapon EquippedWeapon
        +Armor EquippedArmor
        +EquipWeapon(Weapon weapon)
        +EquipArmor(Armor armor)
        +TakeDamage(int damageAmount)
        +SpendAP(int cost)
    }

    class HeroStats {
        <<readonly struct>>
        +int MaxHealth
        +int MaxActionPoints
        +int BaseAttack
        +int BaseDefense
        +int Initiative
        +int MoveSpeed
        +float CritChance
        +float CritMultiplier
    }

    class Weapon {
        +string Name
        +WeaponType Type
        +int BaseDamage
    }

    class Armor {
        +string Name
        +ArmorType Type
        +int Defense
    }

    Hero *-- HeroStats : contains (immutable base)
    Hero o-- Weapon : equips
    Hero o-- Armor : equips
    DamageCalculator ..> Hero : reads stats & calculates damage
```

---

## 📂 Компоненты домена

### 1. Базовые характеристики ([HeroStats.cs](../Assets/Scripts/Core/Stats/HeroStats.cs))
- `readonly struct` — но **только для неизменяемых базовых значений**. `CurrentHealth` и `CurrentActionPoints` сюда не входят — они меняются в течение боя и принадлежат `Hero`, а не struct-снапшоту (см. `code_review_rules.md` §5).
- Содержит:
  - `MaxHealth` — максимум здоровья (не текущее).
  - `MaxActionPoints` — очки действий за ход (AP).
  - `BaseAttack` / `BaseDefense` — атака и защита.
  - `Initiative` — порядок хода в бою.
  - `MoveSpeed` — сколько клеток проходится за 1 AP движения. Разделено с `Initiative`: раньше оба смысла жили в одном поле `Speed`.
  - `CritChance` / `CritMultiplier` — вероятность и множитель критического урона.

### 2. Герой ([Hero.cs](../Assets/Scripts/Core/Stats/Hero.cs))
- Центральный доменный класс. Хранит неизменяемый `BaseStats` и изменяемые `CurrentHealth`/`CurrentActionPoints` как собственные поля с `{ get; private set; }`.
- Отвечает за экипировку оружия ([Weapon.cs](../Assets/Scripts/Core/Items/Weapon.cs)) и брони ([Armor.cs](../Assets/Scripts/Core/Items/Armor.cs)).
- Метод `TakeDamage(int amount)` пересчитывает здоровье без пересоздания всей структуры характеристик.
- Метод `SpendAP(int cost)` — должен вызываться из `ICombatCommand.Execute()`. На момент аудита не подключён нигде (см. `docs/combat_system.md`).

### 3. Калькулятор урона ([DamageCalculator.cs](../Assets/Scripts/Core/Stats/DamageCalculator.cs))
- Чистый статический класс.
- Формула базового урона:
  $$\text{Damage} = \max(1, (\text{Attacker.BaseAttack} + \text{Weapon.BaseDamage}) - (\text{Defender.BaseDefense} + \text{Armor.Defense}))$$
- При крите результат умножается на `CritMultiplier`. `CritChance` на момент аудита не читается никакой логикой — крит приходит готовым `bool` извне, без броска. Первая версия — один тип магического урона поверх физического; полная матрица из 5 типов урона и открытых сопротивлений сознательно отложена (см. `docs/roadmap.md`, таблица вырезанного).

---

## 🔗 Сопутствующие разделы:
- 🏛️ [Архитектура Проекта](./architecture.md)
- 🔄 [Боевая Система и FSM](./combat_system.md)
- 🧝‍♂️ [Расы и Синергия](./races_and_synergies.md)
