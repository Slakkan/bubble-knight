using UnityEngine;

public class AbillityReference : MonoBehaviour
{
    [SerializeField] private Abillity _abillity;

    public Abillity Abillity => _abillity;
}
