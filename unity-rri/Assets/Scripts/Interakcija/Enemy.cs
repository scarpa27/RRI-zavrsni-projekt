using System.Collections;
using Interact;
using UnityEngine;

/* This makes our enemy interactable. */

[RequireComponent(typeof(LikStats))]
public class Enemy : Interaktivno
{
    private LikStats stats;

    private bool umirem = false;

    private void Start()
    {
        stats = GetComponent<LikStats>();
        stats.OnHealthReachedZero += Die;
    }

    // When we interact with the enemy: We attack it.
    public override void Interact()
    {
        CharacterCombat combatManager = Player.instance.playerCombatManager;
        combatManager.Attack(stats);
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
        Player.instance._animator.SetBool("BijeSe",false);
    }
}