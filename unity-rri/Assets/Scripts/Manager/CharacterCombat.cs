using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This resorts combat for all characters. */

[RequireComponent(typeof(LikStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackRate = 1f;
	private float attackCountdown = 0f;

	public event System.Action OnAttack;

	public Transform healthBarPos;

	LikStats myStats;
	LikStats enemyStats;


	void Start ()
	{
		myStats = GetComponent<LikStats>();
		// HealthUIManager.instance.Create (healthBarPos, myStats);
	}

	void Update ()
	{
		attackCountdown -= Time.deltaTime;
	}

	public void Attack (LikStats enemyStats)
	{
		if (attackCountdown > 0f) return;
		this.enemyStats = enemyStats;
		attackCountdown = 1f / attackRate;

		StartCoroutine(DoDamage(enemyStats,.6f));

		OnAttack?.Invoke ();
	}


	IEnumerator DoDamage(LikStats stats, float delay) {
		yield return new WaitForSeconds (delay);

		enemyStats.TakeDamage (myStats.damage.GetValue ());




	}


}
