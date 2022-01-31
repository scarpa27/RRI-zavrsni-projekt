using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCKontoler : MonoBehaviour
{
    public float radiusKretanja;
    public float deltaVrijemeKretanja = 6f;
    private Transform _igrac;
    public bool zanimaGaIgrac = true;
    public bool usePivot = true;
    
    private NavMeshAgent _agent;
    private Vector3 _pivot;
    private Transform _trans;
    private Animator _animator;
    


    private void Start()
    {
        _igrac = Player.instance.transform;
        _agent = GetComponent<NavMeshAgent>();
        
        _pivot = transform.parent.transform.position;
        _pivot = transform.position;
        
        if (GetComponent<Animator>() != null)
            _animator = GetComponent<Animator>();

        StartCoroutine(OdgodiPocetak());
    }

    private IEnumerator Korutina(float delay)
    {
        StopCoroutine(OdgodiPocetak());
        
        while (true)
        {
            _pivot = usePivot ? transform.parent.transform.position : _pivot;
            NovaLokacija(
                zanimaGaIgrac ? 
                    Vector3.Distance(_igrac.position, transform.position) < 20 ? 
                        2 : radiusKretanja 
                    : radiusKretanja);
            
            yield return new WaitForSeconds(delay+Random.Range(-1f,1f));
        }
    }

    private IEnumerator OdgodiPocetak()
    {
        yield return new WaitForSeconds(Random.Range(0, 5.9f));
        // StartCoroutine(AnimatorStanja());
        StartCoroutine(Korutina(deltaVrijemeKretanja));
    }
    
    private IEnumerator AnimatorStanja()
    {
        while (true)
        {
            
            _animator.SetBool("Hoda", _agent.remainingDistance > _agent.stoppingDistance);
            yield return new WaitForSeconds(0.1f);
        }
    }

    
    private void NovaLokacija(float r = 20)
    {
        while (true)
        {
            var centar = _pivot;
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