using UnityEngine;
using UnityEngine.Serialization;

public class AbillityReference : MonoBehaviour
{
    [FormerlySerializedAs("_abillity")] [SerializeField] private Ability ability;

    public Ability Ability => ability;
}
