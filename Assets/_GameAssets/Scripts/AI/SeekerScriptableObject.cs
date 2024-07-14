using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemy/New Enenmy Data")]
public class SeekerScriptableObject : ScriptableObject
{
    [Header("Ai Settings")]
    public LayerMask detectionLayerMask;
    [Space]
    public float speed = 3.0f;
    [Tooltip("This variable is only used when you check off the use nav mesh roation")]
    public float rotationSpeed = 3.5f;
    public float angularSpeed = 120.0f;
    public float acceleration = 7.0f;
    public float stoppingDistance = 0.8f;
    [Space, Range(0.1f, 90.0f)]
    public float detectionAngle = 15.0f;
    public float detectionLength = 5.0f;
    public float chaseTimeAfterVisionLost = 3.0f;
    public float patrolTime = 5.0f;
    public float patrolDistance = 15.0f;
    [Space]
    public float resetAiIfBuggedTime = 5.0f;
    public bool useNavMeshRotation = true;

    [Header("Fonction Utulitaire VARS")]
    public AnimationCurve Chasing;
    public AnimationCurve Sprinting;
    public float maxStamina = 40.0f;
    [Tooltip("the time when the AI is in Idle state where he is preforming an action resting")]
    public float actionTime = 2.3f;
    public float sprintAddedAmount = 3.5f;
    [Range(0.01f, 0.1f)]
    public float staminaTickRate = 0.05f;
    [Range(0, 0.9f)]
    public float restRunPercentage = 0.12f;
}
