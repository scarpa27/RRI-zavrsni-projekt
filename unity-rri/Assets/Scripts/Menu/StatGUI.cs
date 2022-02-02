using System.Collections;
using TMPro;
using UnityEngine;

public class StatGUI : MonoBehaviour
{
    public GameObject zdravlje;
    public GameObject bodovi;
    public GameObject snaga;
    public GameObject stit;

    private PlayerStats _stats;

    private void Start()
    {
        _stats = Player.instance.playerStats;
        StartCoroutine(StatsGUI());
    }


    private IEnumerator StatsGUI()
    {
        while (true)
        {
            _stats = Player.instance.playerStats;
            zdravlje.GetComponent<TextMeshProUGUI>()
                .SetText(("Zdravlje: " + _stats.CurrentHealth + "/" + _stats.maxHealth.GetValue())
                    .Replace("\\n", "\n"));
            bodovi.GetComponent<TextMeshProUGUI>()
                .SetText(("Bodovi: " + _stats.bodovi.GetValue() + "xp").Replace("\\n", "\n"));
            snaga.GetComponent<TextMeshProUGUI>().SetText(("Snaga: " + _stats.damage.GetValue()).Replace("\\n", "\n"));
            stit.GetComponent<TextMeshProUGUI>().SetText(("Štit: " + _stats.armor.GetValue()).Replace("\\n", "\n"));
            // .GetComponent<TextMeshProUGUI>().SetText(("").Replace("\\n","\n"));

            yield return new WaitForSeconds(0.1f);
        }
    }
}