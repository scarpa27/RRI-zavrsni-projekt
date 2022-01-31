using Predmeti;
using UnityEngine;

/* An Item that can be equipped to increase armor/damage. */

[CreateAssetMenu(fileName = "New Item", menuName = "Ruksak/Oprema")]
public class Oprema : Predmet
{
    public EquipmentSlot equipSlot; // What slot to equip it in
    public int armorModifier;
    public int damageModifier;
    public SkinnedMeshRenderer prefab;

    // Called when pressed in the inventory
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