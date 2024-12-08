using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProgressionAi : MonoBehaviour
{
    public Transform[] roamingPoints;
    public float playerDetectionRange = 20f;
    public float stopChaseDistance = 7f;
    public Transform player;
    public GameObject ThrowBomb;
    public GameObject Bomb;
    public float throwHeight = 5f; 
    public float throwSpeed = 10f; 

    private float enemySpeed = 8f;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private int currentRoamingIndex = 0;
    private bool hasThrownBomb = false;

    void Start()
    {
        InitializeEnemy();
    }

    void InitializeEnemy()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemySpeed;

        if (roamingPoints.Length > 0)
        {
            MoveToNextRoamingPoint();
        }
    }
   
    void FixedUpdate()
    {
        HandleEnemyMovement();
       
    }

    void HandleEnemyMovement()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= playerDetectionRange)
            {
                if (distanceToPlayer <= stopChaseDistance)
                {
                    if (!hasThrownBomb)
                    {
                        ThrowBombAtPlayer();
                        hasThrownBomb = true;
                    }
                }
                else
                {
                    ChasePlayer();
                    hasThrownBomb = false;
                }
            }
            else
            {
                Patrol();
                hasThrownBomb = false;
            }
        }
        else
        {
            Patrol();
        }
    }
    void ThrowBombAtPlayer()
    {
        anim.SetBool("Throw", true);
        navMeshAgent.isStopped = true;
        

        if (GrenadePoint.instance.GernadeforEnemy == true)
        {
            
            Vector3 direction = (player.position - ThrowBomb.transform.position).normalized;
            Vector3 initialPosition = ThrowBomb.transform.position + Vector3.up * throwHeight;

            RaycastHit hit;
            if (Physics.Raycast(initialPosition, direction, out hit))
            {
                GameObject instantiatedBomb = Instantiate(Bomb, hit.point, Quaternion.identity);
                Rigidbody bombRigidbody = instantiatedBomb.GetComponent<Rigidbody>();
                if (bombRigidbody != null)
                {
                    float timeToHit = hit.distance / throwSpeed;
                    Vector3 velocity = direction * throwSpeed;
                    bombRigidbody.velocity = velocity;
                }
                GrenadePoint.instance.GernadeforEnemy = false;
            }
          
        }
       


    }


    void ChasePlayer()
    {
        anim.SetBool("Throw", false);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
    }

    void Patrol()
    {
        anim.SetBool("Throw", false);
        navMeshAgent.isStopped = false;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            MoveToNextRoamingPoint();
        }
    }

    void MoveToNextRoamingPoint()
    {
        if (roamingPoints.Length > 0)
        {
            currentRoamingIndex = Random.Range(0, roamingPoints.Length);
            navMeshAgent.SetDestination(roamingPoints[currentRoamingIndex].position);
        }
    }
}
