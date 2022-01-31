using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(CharacterCombat))]
public class EnemyController : MonoBehaviour
{
    public float radiusVida = 10f;
    public float radiusKretanja = 20f;
    public float deltaVrijemeKretanja = 6f;
    private NavMeshAgent _agent;
    private Transform _target;
    private Vector3 _pivot;
    private CharacterCombat _mojCharacterCombat;
    private Animator _animator;
    // private Player _igrac;


    public bool zanimaGaIgrac = true;
    public bool usePivot = true;
    

    private void Start()
    {
        _target = Player.instance.transform;
        _agent = GetComponent<NavMeshAgent>();
        _mojCharacterCombat = GetComponent<CharacterCombat>();
        _animator = GetComponent<Animator>();
        StartCoroutine(OdgodiPocetak());
    }

    private void Update()
    {
        
        
        float distance = Vector3.Distance(_target.position, transform.position);
        
        if (distance > radiusVida) return;
        
        _agent.SetDestination(_target.position);
        if (distance <= 3)
        {
            _mojCharacterCombat.Attack(Player.instance.playerStats);
            Player.instance._animator.SetBool("BijeSe",true);
            _animator.SetBool("BijeSe",true);
        }
        else
        {
            Player.instance._animator.SetBool("BijeSe",false);
            _animator.SetBool("BijeSe",false);
        }
        
        FaceTarget();
    }
    
    private IEnumerator Korutina(float delay)
    {
        StopCoroutine(OdgodiPocetak());
        
        while (true)
        {
            _pivot = transform.parent.transform.position;
            NovaLokacija(radiusKretanja);
            yield return new WaitForSeconds(delay+Random.Range(-1f,1f));
        }
    }

    private IEnumerator OdgodiPocetak()
    {
        yield return new WaitForSeconds(Random.Range(0, 5.9f));
        StartCoroutine(AnimatorStanja());
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
            var centar = usePivot ? _pivot : transform.position;
            var cilj = new Vector3(centar.x + Random.Range(-r, r), centar.y, centar.z + Random.Range(-r, r));

            NavMeshHit hit;
            if (NavMesh.SamplePosition(cilj, out hit, 50, 7))
            {
                _agent.SetDestination(hit.position);
                _animator.SetBool("Hoda",true);
            }
            else
                continue;
            break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusVida);
    }

    // Point towards the player
    private void FaceTarget()
    {
        var direction = (_target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }
}