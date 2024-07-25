using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Player Data", menuName ="Player/New Player Data")]
public class PlayerDataScriptableObject : ScriptableObject
{
    public float speed;
    public float speedModifier;
    public float rotationSpeed;
}
