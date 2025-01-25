using UnityEngine;

public class EnemyReference : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    public Enemy Enemy => _enemy;
}
