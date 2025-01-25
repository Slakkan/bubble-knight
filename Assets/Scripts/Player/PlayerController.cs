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

    [SerializeField] private Abillity _abillitySlash;

    [SerializeField] private Abillity _abillitySmash;

    [SerializeField] private Animator _animator;

    private Vector2 _movmentVector;
    private bool _isMousePressed = false;

    [SerializeField] private TriggerCollider _hitBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction.action.actionMap.Enable();
        agent.updateRotation = false;
        _moveAction.action.started += MovementChanged;

        _abillitySlashAction.action.started += OnAbillitySlashPressed;
        _abillitySmashAction.action.started += OnAbillitySmashPressed;

        _hitBox.TriggerEntered += TriggerEnterHandler;
    }

    private void OnDestroy()
    {
        _moveAction.action.started -= MovementChanged;

        _abillitySlashAction.action.started -= OnAbillitySlashPressed;
        _abillitySmashAction.action.started -= OnAbillitySmashPressed;
    }

    private void MovementChanged(InputAction.CallbackContext ctx)
    {
        _movmentVector = _moveAction.action.ReadValue<Vector2>();
    }

    private void OnAbillitySlashPressed(InputAction.CallbackContext ctx)
    {
        _abillitySlash.Cast();
        _animator.SetTrigger("SlashTrigger");
    }

    private void OnAbillitySmashPressed(InputAction.CallbackContext ctx)
    {
        _abillitySmash.Cast();
        _animator.SetTrigger("SmashTrigger");
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
        if (!other.gameObject.GetComponent<Enemy>())
        {
            return;
        }
        
        // DIE
    }
}