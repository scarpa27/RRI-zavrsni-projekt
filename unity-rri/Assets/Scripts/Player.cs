using System.Collections;
using Interact;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public delegate void OnFocusChange(Interaktivno novi);

    public Interaktivno fokus;
    public PlayerStats playerStats;
    public CharacterCombat playerCombatManager;

    public LayerMask movementMask;
    public LayerMask interactionMask;


    private NavMeshAgent _navMeshAgent;
    private Transform _point;
    public Animator _animator;
    public OnFocusChange FocusCallback;


    private void Start()
    {
        playerStats.OnHealthReachedZero += Die;
        playerCombatManager = GetComponent<CharacterCombat>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        FocusCallback += OnFocusChanged;
        _animator = GetComponent<Animator>();
        StartCoroutine(AnimatorStanja());
    }

    // Update is called once per frame
    private void Update()
    {
        if (_point != null)
        {
            Hodaj(_point.position);
            Gledaj();
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
            NadiMetu();

        if (Input.GetMouseButtonDown(1))
            ProbajInterakciju();
    }

    private void NadiMetu()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, movementMask))
        {
            Hodaj(hit.point);
            SetFocus(null);
        }
    }

    private void ProbajInterakciju()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactionMask))
            SetFocus(hit.collider.GetComponent<Interaktivno>());
    }

    private void SetFocus(Interaktivno novi)
    {
        FocusCallback?.Invoke(novi);


        if (fokus != novi && fokus != null)
            fokus.OnDefocused();

        fokus = novi;

        if (fokus != null)
            fokus.OnFocused(transform);
    }

    private void Hodaj(Vector3 dest)
    {
        _navMeshAgent.SetDestination(dest);
    }

    private void OnFocusChanged(Interaktivno novi)
    {
        if (novi != null)
        {
            _navMeshAgent.stoppingDistance = novi.radius * 0.92f;
            _navMeshAgent.updateRotation = false;
            _point = novi.trans;
        }
        else
        {
            _navMeshAgent.stoppingDistance = 0;
            _navMeshAgent.updateRotation = true;
            _point = null;
        }
    }

    

    private void Gledaj()
    {
        var smjer = (_point.position - transform.position).normalized;
        var rot = Quaternion.LookRotation(new Vector3(smjer.x, 0, smjer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 4f);
    }
    
    private IEnumerator AnimatorStanja()
    {
        while (true)
        {
            // _animator.SetBool("BijeSe",false);
            _animator.SetBool("Hoda", _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #region Singleton

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
}