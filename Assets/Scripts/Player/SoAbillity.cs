using UnityEngine;

[CreateAssetMenu(fileName = "SoAbillity", menuName = "Scriptable Objects/SoAbillity")]
public class SoAbillity : ScriptableObject
{
    public string Name;
    
    public int Damage;
    public float KnockBack;
    [Header("Custom additional CD")]
    public float Cooldown;
    
    [Header("Just for spheric colliders.")]
    public float Range;

    [Header("Get this value from the Animator")]
    public float AnimationSpeed;
    
    [Header("Get this value from the associated AnimationClip. Just for Reference.")]
    public float AnimationLength;

    public float CastTime; // Is equivalent to animation



    

    public float TimeToReachMaxRange;


}
