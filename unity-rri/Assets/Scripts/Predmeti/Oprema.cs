using Predmeti;
using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Ruksak/Oprema")]
public class Oprema : Predmet
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;
    public SkinnedMeshRenderer prefab;

    public override void Use()
    {
        OpremaManager.instance.Equip(this); // Equip
        RemoveFromInv(); // Remove from inventory
    }
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
}