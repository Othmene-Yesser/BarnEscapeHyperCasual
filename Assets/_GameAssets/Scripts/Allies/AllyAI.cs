using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(SphereCollider), typeof(ParticleSystem))]
public class AllyAI : MonoBehaviour
{
    Animator animator;
    NavMeshAgent allyAgent;
    GameManager gameManager;
    Transform player;
    CapsuleCollider sphereCollider;
    ParticleSystem collectedVFX;
    ParticleSystem idleVFX;
    AudioManager audioManager;
    
    [SerializeField] float stoppingDistance = 3f;
    [SerializeField] float speed = 5.5f;
    [SerializeField] float distance;
    public bool alive = true;
    public bool hasBeenCaptured;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        allyAgent = GetComponent<NavMeshAgent>();
        sphereCollider = transform.GetChild(0).GetComponent<CapsuleCollider>();
        animator = transform.GetChild(1).GetComponent<Animator>();
        collectedVFX = GetComponent<ParticleSystem>();
        audioManager = FindObjectOfType<AudioManager>();
        idleVFX = transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        allyAgent.stoppingDistance = stoppingDistance;
        allyAgent.speed = speed;
        sphereCollider.enabled = hasBeenCaptured;
        alive = true;
        idleVFX.Play();
    }

    private void FixedUpdate()
    {
        if (hasBeenCaptured && alive)
        {
            allyAgent.SetDestination(player.position);
            distance = Vector3.Distance(transform.position, player.position);
            if (distance > stoppingDistance)
            {
                animator.SetFloat(StringsAndConsts.BlendTree1D, 1);
            }
            else
            {
                float clamped = Mathf.Clamp01(distance - 2f);
                animator.SetFloat(StringsAndConsts.BlendTree1D, clamped);
            }
        }
    }

    private void Die()
    {
        this.gameManager.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringsAndConsts.SeekerTag) && hasBeenCaptured == true && alive)
        {
            //! Player death animation
            animator.Play("Death");
            alive = false;
            sphereCollider.enabled = false;
            allyAgent.SetDestination(transform.position);
            Debug.Log("Died");
            SeekerAI seeker = other.GetComponent<SeekerAI>();
            seeker.StartCoroutine(seeker.IdleForXAmountOfSeconds(1.5f));
            Invoke(nameof(Die), 3f);
            gameManager.CheckIfLostManyAllies();
        }

        if (other.CompareTag(StringsAndConsts.PlayerTag) && hasBeenCaptured == false)
        {
            Destroy(idleVFX.gameObject);
            audioManager.PlaySoundEffect(audioManager.collectAlly);
            Debug.Log("Captured " + this.name);
            collectedVFX.Play();
            player = other.transform;
            hasBeenCaptured = true;
            sphereCollider.enabled = hasBeenCaptured;
            gameManager.CheckIfCollectedAllALlies();
        }
        
    }
}
