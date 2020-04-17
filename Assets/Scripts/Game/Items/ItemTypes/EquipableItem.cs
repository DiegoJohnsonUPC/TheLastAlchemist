using UnityEngine;
using TLA.CharacterStats;
public enum EquipmentType
{
    Helmet,
    Chestpiece,
    Gloves,
    Boots,
    Accesory1,
    Accesory2,
    Weapon1,
    Weapon2
}

[CreateAssetMenu]
public class EquipableItem : Item
{
    public int DamageBonus;
    [Space]
    public float DamagePercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(Character c)
    {
        if (DamageBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
        if (DamagePercentBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMult, this));
    }
    public void Unequip(Character c)
    {
        c.Damage.RemoveAllModifiersFromSource(this);
    }
}
