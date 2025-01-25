using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Moskito = 0,
    SeaUrchin = 1
}
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private EnemyType _type;
    public EnemyType Type => _type;
    private Transform _player;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private SoEnemy _enemyData;

    public SoEnemy EnemyData => _enemyData;

    private int _currentHealth;

    [SerializeField] private TriggerCollider _hitBox;

    [SerializeField] private TriggerCollider _playerNearCollider;

    private ScoreController _scoreController;

    public void Init(Transform PlayerTransform, ScoreController scoreController)
    {
        _currentHealth = _enemyData.Health;
        _scoreController = scoreController;
        _player = PlayerTransform;

        _hitBox.TriggerEntered += TriggerEnteredHandler;
        if (_playerNearCollider)
        {
            _playerNearCollider.TriggerEntered += PlayerNearHandler;
        }
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
            _currentHealth -= a.AbilityData.Damage;
            if (_currentHealth <= 0)
            {
                _scoreController.AddScore();
                Destroy(gameObject);
            }

            Vector3 knockBackVector = (transform.position - _player.position).normalized * a.AbilityData.KnockBack *
                                      (1 - _enemyData.KnockBackResistance);
            _agent.velocity = knockBackVector;
        }
    }

    private void PlayerNearHandler(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _agent.velocity *= 1.8f;
        }
    }
}