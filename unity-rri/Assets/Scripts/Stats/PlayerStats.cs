public class PlayerStats : LikStats
{
    public GeneralStat bodovi;

    public override void Start()
    {
        base.Start();
        OpremaManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Oprema newE, Oprema oldE)
    {
        if (newE != null)
        {
            armor.AddModifier(newE.armorModifier);
            damage.AddModifier(newE.damageModifier);
        }

        if (oldE != null)
        {
            armor.RemoveModifier(oldE.armorModifier);
            damage.RemoveModifier(oldE.damageModifier);
        }
    }
}