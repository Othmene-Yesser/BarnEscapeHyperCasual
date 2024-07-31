using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class SeekerAI : MonoBehaviour
{
    [SerializeField] States state;
    private States State
    {
        get
        {
            return state;
        }
        set
        {
            if (state != value)
            {
                switch (value)
                {
                    case States.Idle:
                        animator.SetFloat(Strings.BlendTree1D, 0);
                        break;
                    case States.Patrol:
                        enemyAgent.speed = enemyData.walkSpeed;
                        animator.SetFloat(Strings.BlendTree1D, 1);
                        break;
                    case States.Chase:
                        enemyAgent.speed = enemyData.runSpeed;
                        animator.SetFloat(Strings.BlendTree1D, 2);
                        break;
                    case States.Searching:
                        enemyAgent.speed = enemyData.runSpeed;
                        animator.SetFloat(Strings.BlendTree1D, 2);
                        break;
                    case States.Sprint:
                        enemyAgent.speed = enemyData.sprintSpeed;
                        animator.SetFloat(Strings.BlendTree1D, 2);
                        break;
                }
                state = value;
            } 
        }
    }
    
    NavMeshAgent enemyAgent;
    Animator animator;
    PlayerBuffManager playerbuffs;
    PlayerManager playerManager;
    GameManager gameManager;

    [SerializeField] SeekerScriptableObject enemyData;

    Transform rayShootPoint;

    [SerializeField] Vector3 lastSeenPosition;

    [SerializeField] bool playerInVision = false;

    [SerializeField] float stamina;

    private enum States
    {
        Idle,
        Patrol,
        Chase,
        Searching,
        Sprint,
    }

    //! Player Detection Variables
    bool[] rayStatus;

    Ray[] rays;
    RaycastHit[] hits;

    bool timerDoneSearch;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerbuffs = FindObjectOfType<PlayerBuffManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        animator.SetFloat(Strings.BlendTree1D, 1);
        State = States.Patrol;

        rayStatus = new bool[17];
        rays = new Ray[17];
        hits = new RaycastHit[17];
        rayShootPoint = transform.GetChild(0);

        State = States.Patrol;
        StartCoroutine(Patrol());
        timerDoneSearch = false;

        stamina = enemyData.maxStamina;

        enemyAgent.stoppingDistance = enemyData.stoppingDistance;
        enemyAgent.speed = enemyData.walkSpeed;
    }
    private void Update()
    {
        playerInVision = CanSeePlayer();
    }

    #region Methods

    public void Idle()
    {
        StopAllCoroutines();
        enemyAgent.SetDestination(transform.position);
        animator.SetFloat(Strings.BlendTree1D, 0);
    }

    float DistanceFromLastRegisteredPosition(bool patrol = false)
    {
        Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
        if (patrol)
        {
            Vector3 _destination = new Vector3(enemyAgent.destination.x, 0, enemyAgent.destination.z);
            return Vector3.Distance(_destination, position);
        }
        Vector3 destination = new Vector3(lastSeenPosition.x, 0, lastSeenPosition.z);
        return Vector3.Distance(destination, position);
    }

    bool CheckIfNeededToIdle()
    {
        float stamina = CalculatePercentage(this.stamina, enemyData.maxStamina);
        if (stamina <= enemyData.restRunPercentage)
        {
            this.stamina = enemyData.maxStamina;
            return true;
        }
        else
        {
            return false;
        }
    }

    Vector3 GenerateRandomPosition()
    {
        float randomX = Random.Range(-enemyData.patrolDistance, enemyData.patrolDistance);
        float randomZ = Random.Range(-enemyData.patrolDistance, enemyData.patrolDistance);
        return new Vector3(randomX + transform.position.x, 0, randomZ + transform.position.z);
    }

    States UtilityFunctionDecision()
    {
        float distance = DistanceFromLastRegisteredPosition();
        float sprintValue = enemyData.Sprinting.Evaluate(CalculatePercentage(distance, enemyData.detectionLength));
        float shootingValue = enemyData.Chasing.Evaluate(CalculatePercentage(distance, enemyData.detectionLength));

        if (sprintValue > shootingValue)
        {
            //! Sprint
            return States.Sprint;
        }
        else
        {
            //! Shoot
            return States.Chase;
        }
    }

    float CalculatePercentage(float value, float maxValue)
    {
        var percentage = (value * 100 / maxValue) / 100;
        return percentage;
    }
    bool CanSeePlayer()
    {
        int i = 0;

        rays[i] = new Ray(rayShootPoint.position, rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.red);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 1 / 2), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 3 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 1 / 4), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 1 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 3 / 4), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 5 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (enemyData.detectionAngle * 7 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, -(enemyData.detectionAngle * 1 / 2), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, -(enemyData.detectionAngle * 3 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (-enemyData.detectionAngle * 1 / 4), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (-enemyData.detectionAngle * 1 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (-enemyData.detectionAngle * 3 / 4), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, -(enemyData.detectionAngle * 5 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, (-enemyData.detectionAngle * 7 / 8), 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.blue);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, enemyData.detectionAngle, 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.red);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);
        i++;

        rays[i] = new Ray(rayShootPoint.position, Quaternion.Euler(0, -enemyData.detectionAngle, 0) * rayShootPoint.forward);
        Debug.DrawRay(rays[i].origin, rays[i].direction * enemyData.detectionLength, Color.red);
        rayStatus[i] = Physics.Raycast(rays[i], out hits[i], enemyData.detectionLength, enemyData.detectionLayerMask);

        i = 0;
        while (i < rays.Length)
        {
            if (CheckIfViableForDetection(i))
            {
                lastSeenPosition = hits[i].point;
                return true;
            }
            i++;
        }
        return false;
    }

    private bool CheckIfViableForDetection(int i)
    {
        Collider other = hits[i].collider;
        if (rayStatus[i] && (other.CompareTag(Strings.PlayerTag) || other.CompareTag(Strings.AllyTag)))
        {
            if (playerbuffs.Invisible)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Killed Player");
        playerManager.isAlive = false;
        playerManager.animatorManager.PlayTargetAnimation("Death");
        StopCoroutine(gameManager.gameTimeTicker);
        gameManager.Lose();
    }


    #endregion

    #region IEnumerators

    IEnumerator IdleForXAmountOfSeconds(float time = 0.1f)
    {
        enemyAgent.SetDestination(transform.position);
        yield return new WaitForSeconds(time);
        State = States.Patrol;
        StartCoroutine(Patrol());
    }
    IEnumerator Sprint()
    {
        while (playerInVision && State == States.Sprint)
        {
            //! Sprint
            stamina -= enemyData.staminaTickRate;
            yield return null;
        }
    }

    IEnumerator Patrol()
    {
        float timer = enemyData.patrolTime;
        enemyAgent.stoppingDistance = enemyData.stoppingDistance;
        while (State == States.Patrol)
        {
            if (timer >= enemyData.patrolTime || DistanceFromLastRegisteredPosition(true) <= enemyData.stoppingDistance)
            {
                timer = 0f;
                enemyAgent.SetDestination(GenerateRandomPosition());
            }
            timer += Time.deltaTime;

            yield return null;

            if (playerInVision)
            {
                State = States.Chase;
                StartCoroutine(Chase());
                break;
            }
        }
    }

    IEnumerator Chase()
    {
        enemyAgent.stoppingDistance = enemyData.stoppingDistance * 3;
        while (State == States.Chase || State == States.Sprint)
        {
            enemyAgent.SetDestination(lastSeenPosition);
            if (Vector3.Distance(transform.position, enemyAgent.destination) <= enemyAgent.stoppingDistance - 0.4f)
            {
                animator.SetFloat(Strings.BlendTree1D, 0);
                //! Check if we go close to the player the we kill him
                Collider[] collidersNear = Physics.OverlapSphere(transform.position, enemyAgent.stoppingDistance);
                foreach (Collider collider in collidersNear)
                {
                    if (collider.CompareTag(Strings.PlayerTag) && playerManager.isAlive)
                    {

                        KillPlayer();
                    }
                }
            }
            else
            {
                animator.SetFloat(Strings.BlendTree1D, 2);
            }

            //! Do Action
            States decidedState = UtilityFunctionDecision();
            //! Only change the state when there is a different one 
            switch (decidedState)
            {
                case States.Sprint:
                    //! If he is sprinting then he won't need to sprint
                    if (State == States.Sprint)
                        break;
                    State = States.Sprint;
                    StartCoroutine(Sprint());
                    break;
                case States.Chase:
                    State = States.Chase;
                    break;
                default:
                    Debug.LogError("What ??");
                    break;
            }

            //! Check if needed to rest or reload
            bool idle = CheckIfNeededToIdle();
            if (idle)
            {
                State = States.Idle;
                StartCoroutine(IdleForXAmountOfSeconds(enemyData.actionTime));
            }

            yield return null;

            if (!CanSeePlayer())
            {
                State = States.Searching;
                StartCoroutine(Search());
            }
        }
    }
    IEnumerator Search()
    {
        enemyAgent.SetDestination(lastSeenPosition);
        enemyAgent.stoppingDistance = enemyData.stoppingDistance;
        yield return null;
        IEnumerator timer = SearchTimer();
        StartCoroutine(timer);
        while (State == States.Searching)
        {
            yield return null;
            if (DistanceFromLastRegisteredPosition() <= enemyData.stoppingDistance || timerDoneSearch)
            {
                State = States.Patrol;
                StartCoroutine(Patrol());
                break;
            }
            else if (playerInVision)
            {
                State = States.Chase;
                StartCoroutine(Chase());
                break;
            }
        }
        StopCoroutine(timer);
    }

    IEnumerator SearchTimer()
    {
        yield return new WaitForSeconds(enemyData.patrolTime);
        if (State == States.Searching)
        {
            timerDoneSearch = true;
        }
        else
        {
            Debug.Log("Reached Dest");
        }
    }

    #endregion
}
