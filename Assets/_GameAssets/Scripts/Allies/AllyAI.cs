using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider))]
public class AllyAI : MonoBehaviour
{
    //GameManager gameManager;
    Animator animator;

    NavMeshAgent allyAgent;
    GameManager gameManager;

    Transform player;

    CapsuleCollider sphereCollider;

    public bool hasBeenCaptured = false;
    [SerializeField] float stoppingDistance = 3f;
    [SerializeField] float speed = 5.5f;
    [SerializeField] float distance;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        allyAgent = GetComponent<NavMeshAgent>();
        sphereCollider = transform.GetChild(0).GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        allyAgent.stoppingDistance = stoppingDistance;
        allyAgent.speed = speed;
        sphereCollider.enabled = hasBeenCaptured;
    }

    private void FixedUpdate()
    {
        if (hasBeenCaptured)
        {
            allyAgent.SetDestination(player.position);
            distance = Vector3.Distance(transform.position, player.position);
            if (distance > stoppingDistance)
            {
                animator.SetFloat(Strings.BlendTree1D, 1);
            }
            else
            {
                float clamped = Mathf.Clamp01(distance - 2f);
                animator.SetFloat(Strings.BlendTree1D, clamped);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Strings.SeekerTag) && hasBeenCaptured == true)
        {
            //! Player death animation
            Destroy(this.gameObject);
        }

        if (other.CompareTag(Strings.PlayerTag) && hasBeenCaptured == false)
        {
            player = other.transform;
            hasBeenCaptured = true;
            sphereCollider.enabled = hasBeenCaptured;
            gameManager.CheckIfCollectedAllALlies();
        }
        
    }
}
