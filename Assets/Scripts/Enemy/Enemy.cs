using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform _player;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private SoEnemy _enemyData;

    private int _currentHealth;

    [SerializeField]
    private TriggerCollider _hitBox;

    public void Init(Transform PlayerTransform)
    {
        _currentHealth = _enemyData.Health;
        _player = PlayerTransform;

        _hitBox.TriggerEntered += TriggerEnteredHandler;
    }

    private void Update()
    {
        _agent.destination = _player.position;
    }

    private void TriggerEnteredHandler(Collider other)
    {
        if (other.transform.TryGetComponent(out AbillityReference ar))
        {
            Abillity a = ar.Abillity;
            if (a._hitEnemysWithLastCast.Contains(this))
            {
                return;
            }

            a._hitEnemysWithLastCast.Add(this);
            _currentHealth--;
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
            Vector3 knockBackVector = (transform.position - _player.position).normalized * a.AbilityData.KnockBack *
                                      (1-_enemyData.KnockBackResistance);
            _agent.velocity = knockBackVector;
        }
    }
}