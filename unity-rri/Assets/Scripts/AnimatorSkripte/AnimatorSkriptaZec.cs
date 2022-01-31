using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatorSkriptaZec : MonoBehaviour
{
    
    
    private Animator _animator;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        
        StartCoroutine(OdgodiPocetak());
    }
    
    private IEnumerator OdgodiPocetak()
    {
        yield return new WaitForSeconds(Random.Range(0, 5.9f));
        StartCoroutine(AnimatorStanja());
    }
    
    private IEnumerator AnimatorStanja()
    {
        StopCoroutine(OdgodiPocetak());
        while (true)
        {
            _animator.SetBool("Hoda", _agent.remainingDistance > _agent.stoppingDistance+1);
            yield return new WaitForSeconds(0.1f);
        }
    }

    
}
