using UnityEngine;

[CreateAssetMenu(fileName = "SoWave", menuName = "Scriptable Objects/SoWave")]
public class SoWave : ScriptableObject
{
    public int AmountOfMoskitos;
    public int AmountOfSeaUrchin;
    public float TimeBeforeNextWave;
}
