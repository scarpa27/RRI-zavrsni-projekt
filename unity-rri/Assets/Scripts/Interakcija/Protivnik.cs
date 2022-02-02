using System.Collections;
using Interact;
using UnityEngine;

[RequireComponent(typeof(LikStats))]
public class Protivnik : Interaktivno
{
    private LikStats stats;

    private bool umirem;

    private void Start()
    {
        stats = GetComponent<LikStats>();
        stats.OnHealthReachedZero += Die;
    }

    public override void Interact()
    {
        var borba = Player.instance.playerCombatManager;
        borba.Attack(stats);
    }

    private void Die()
    {
        if (umirem) return;
        StartCoroutine(Umri());
    }

    private IEnumerator Umri()
    {
        umirem = true;
        Player.instance.playerStats.damage.AddModifier(1f);
        Player.instance.playerStats.bodovi.AddModifier(20f);

        GetComponent<Animator>()?.SetTrigger("Umri");
        yield return new WaitForSeconds(3f);
        if (gameObject != null) Destroy(gameObject);
        Player.instance._animator.SetBool("BijeSe", false);
    }
}