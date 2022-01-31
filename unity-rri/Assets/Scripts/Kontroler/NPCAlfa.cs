using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Kontroler
{
    
    
    public class NPCAlfa : MonoBehaviour
    {
        public bool miciSe;
        public float radijus = 20;
        private NavMeshAgent _agent;
        
        private void Start()
        {
            if (!miciSe) return;
            _agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Korutina());
        }
        
        private IEnumerator Korutina(float delay = 6f)
        {
            while (true)
            {
                NovaLokacija(radijus);
                yield return new WaitForSeconds(delay);
            }
        }
        
        private void NovaLokacija(float r)
        {
            while (true)
            {
                var centar = transform.position;
                var cilj = new Vector3(centar.x + Random.Range(-r, r), centar.y, centar.z + Random.Range(-r, r));
                

                NavMeshHit hit;
                if (NavMesh.SamplePosition(cilj, out hit, 50, 7))
                    _agent.SetDestination(hit.position);
                else
                    continue;
                break;
            }
        }
    }
}