using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform _player;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private SoEnemy _enemyData;
    
    public SoEnemy EnemyData => _enemyData;

    private int _currentHealth;

    [SerializeField]
    private TriggerCollider _hitBox;

    [SerializeField]
    private TriggerCollider _playerNearCollider;

    public void Init(Transform PlayerTransform)
    {
        _currentHealth = _enemyData.Health;
        _player = PlayerTransform;

        _hitBox.TriggerEntered += TriggerEnteredHandler;
        _playerNearCollider.TriggerEntered += PlayerNearHandler;
    }

    private void Update()
    {
        _agent.destination = _player.position;
    }

    private void TriggerEnteredHandler(Collider other)
    {
        if (other.transform.TryGetComponent(out AbillityReference ar))
        {
            Ability a = ar.Ability;
            if (a._hitEnemysWithLastCast.Contains(this))
            {
                return;
            }

            a._hitEnemysWithLastCast.Add(this);
            _currentHealth-= a.AbilityData.Damage;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            Vector3 knockBackVector = (transform.position - _player.position).normalized * a.AbilityData.KnockBack *
                                      (1-_enemyData.KnockBackResistance);
            _agent.velocity = knockBackVector;
        }
    }

    private void PlayerNearHandler(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.velocity *= 3f;
        }
        
    }
}