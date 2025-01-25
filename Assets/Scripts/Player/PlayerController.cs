using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;

    [FormerlySerializedAs("_abillityOneAction")] [SerializeField]
    private InputActionReference _abillitySlashAction;

    [SerializeField] private InputActionReference _abillitySmashAction;

    [SerializeField] private NavMeshAgent agent;

    [FormerlySerializedAs("_abillitySlash")] [SerializeField] private Ability abilitySlash;

    [FormerlySerializedAs("_abillitySmash")] [SerializeField] private Ability abilitySmash;

    [SerializeField] private Animator _animator;

    private Vector2 _movmentVector;
    private bool _isMousePressed = false;

    [SerializeField] private TriggerCollider _hitBox;

    private int _health;
    private bool _isExecutingAbility = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction.action.actionMap.Enable();
        agent.updateRotation = false;
        _moveAction.action.started += MovementChanged;

        _abillitySlashAction.action.started += OnAbillitySlashPressed;
        _abillitySmashAction.action.started += OnAbillitySmashPressed;

        _hitBox.TriggerEntered += TriggerEnterHandler;

        abilitySlash.Finished += AbilityFinishedHandler;
        abilitySmash.Finished += AbilityFinishedHandler;
    }

    private void OnDestroy()
    {
        _moveAction.action.started -= MovementChanged;

        _abillitySlashAction.action.started -= OnAbillitySlashPressed;
        _abillitySmashAction.action.started -= OnAbillitySmashPressed;
        
        _hitBox.TriggerEntered -= TriggerEnterHandler;

        abilitySlash.Finished -= AbilityFinishedHandler;
        abilitySmash.Finished -= AbilityFinishedHandler;
    }

    private void MovementChanged(InputAction.CallbackContext ctx)
    {
        _movmentVector = _moveAction.action.ReadValue<Vector2>();
    }

    private void AbilityFinishedHandler(Ability a)
    {
        _isExecutingAbility = false;
    }
    
    private void OnAbillitySlashPressed(InputAction.CallbackContext ctx)
    {
        if (!_isExecutingAbility && abilitySlash.TryCast())
        {
            _isExecutingAbility = true;
            _animator.SetTrigger("SlashTrigger");
        }
        
    }

    private void OnAbillitySmashPressed(InputAction.CallbackContext ctx)
    {
        if (!_isExecutingAbility && abilitySmash.TryCast())
        {
            _isExecutingAbility = true;
            _animator.SetTrigger("SmashTrigger");
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _movmentVector = _moveAction.action.ReadValue<Vector2>();
        agent.destination = transform.position + new Vector3(_movmentVector.x * 3, 0f, _movmentVector.y * 3);
        if (_movmentVector.sqrMagnitude > 0f)
        {
            agent.transform.rotation = Quaternion.LookRotation(new Vector3(_movmentVector.x, 0f, _movmentVector.y));
        }
    }

    private void TriggerEnterHandler(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Enemy e))
        {
            return;
        }

        _health -= e.EnemyData.Damage;

        if (_health <= 0)
        {
            // DIE
        }
        
    }
}