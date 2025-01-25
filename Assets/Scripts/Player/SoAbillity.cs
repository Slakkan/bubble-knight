using UnityEngine;

[CreateAssetMenu(fileName = "SoAbillity", menuName = "Scriptable Objects/SoAbillity")]
public class SoAbillity : ScriptableObject
{
    public string Name;
    
    public int Damage;

    public float CastTime; // Is equivalent to animation

    public float KnockBack;

    public float Range;

    public float TimeToReachMaxRange;

    public bool UseAnimatedCollider;
}
