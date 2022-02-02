using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AnimatorSkripte
{
    public class AnimatorSkriptaSeljak : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();

            StartCoroutine(OdgodiPocetak());
        }

        private IEnumerator OdgodiPocetak()
        {
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            StartCoroutine(AnimatorStanja());
        }

        private IEnumerator AnimatorStanja()
        {
            StopCoroutine(OdgodiPocetak());
            while (true)
            {
                _animator.SetBool("Hoda", _agent.remainingDistance > _agent.stoppingDistance + 0.4);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}