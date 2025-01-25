using UnityEngine;

[CreateAssetMenu(fileName = "SoEnemy", menuName = "Scriptable Objects/SoEnemy")]
public class SoEnemy : ScriptableObject
{
    public int Health;

    public float KnockBackResistance;
    
    public int Damage;
}
