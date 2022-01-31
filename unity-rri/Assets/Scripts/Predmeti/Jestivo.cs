using Predmeti;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Ruksak/Jestivo")]
public class Jestivo : Predmet
{
    public float healthGain;
    public float damageGain;
    public float maxHealthGain;
    public float armorGain;

    public override void Use()
    {
        var playerStats = Player.instance.playerStats;
        playerStats.Heal(healthGain);
        playerStats.damage.AddModifier(damageGain);
        playerStats.maxHealth.AddModifier(maxHealthGain);
        playerStats.armor.AddModifier(armorGain);
        RemoveFromInv();
    }
}