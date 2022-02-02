using System;
using System.Collections;
using UnityEngine;

/* This resorts combat for all characters. */

[RequireComponent(typeof(LikStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackRate = 1f;
    private float attackCountdown;
    private LikStats enemyStats;


    private LikStats myStats;


    private void Start()
    {
        myStats = GetComponent<LikStats>();
    }

    private void Update()
    {
        attackCountdown -= Time.deltaTime;
    }

    public event Action OnAttack;

    public void Attack(LikStats enemyStats)
    {
        if (attackCountdown > 0f) return;
        this.enemyStats = enemyStats;
        attackCountdown = 1f / attackRate;

        StartCoroutine(RaniGa(enemyStats, .6f));

        OnAttack?.Invoke();
    }


    private IEnumerator RaniGa(LikStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        enemyStats.TakeDamage(myStats.damage.GetValue());
    }
}