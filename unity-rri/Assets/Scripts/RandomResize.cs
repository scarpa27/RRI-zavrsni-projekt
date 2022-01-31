using UnityEngine;
using UnityEngine.AI;

public class RandomResize : MonoBehaviour
{
    public float velicina = 0.15f;
    public float brzina = 2f;
       private void Start()
       {
           var rnd = Random.Range(-velicina, velicina);
           transform.localScale += new Vector3(rnd,rnd,rnd);
           rnd = Random.Range(-brzina, brzina);
           GetComponent<NavMeshAgent>().speed += rnd;
       }
       
   }
