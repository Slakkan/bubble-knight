using System;
using System.Collections;
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

    [FormerlySerializedAs("_abillitySlash")] [SerializeField]
    private Ability abilitySlash;

    [FormerlySerializedAs("_abillitySmash")] [SerializeField]
    private Ability abilitySmash;

    [SerializeField] private Animator _animator;

    private Vector2 _movmentVector;
    private bool _isMousePressed = false;

    [SerializeField] private TriggerCollider _hitBox;

    private int _health = 5;
    private bool _isExecutingAbility = false;

    [SerializeField] private GameObject[] _disableOnDeath;

    [SerializeField] private GameObject _model;

    public static int Score = 0;

    public event Action<int> OnHealthChanged;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Score = 0;
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

        
        Vector3 mousePos = Input.mousePosition;
        Ray r = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(r, out RaycastHit h, 100))
        {
            agent.transform.LookAt(h.point);
            Vector3 newForward = h.point - agent.transform.position;
            agent.transform.forward = (new Vector3(newForward.x, 0f, newForward.z)).normalized;
        }
    }

    private bool isInvincible = false;
    
    private void TriggerEnterHandler(Collider other)
    {
        if (isInvincible)
        {
            return;
        }
        if (!other.gameObject.TryGetComponent(out EnemyReference er))
        {
            return;
        }

        if (er.Enemy.Type is EnemyType.Moskito)
        {
            if (Vector3.Dot(er.Enemy.transform.forward, transform.position - er.Enemy.transform.position ) < 0)
            {
                // Moskiots cant bite backwards
                return;
            }
        }

        _health -= er.Enemy.EnemyData.Damage;
        OnHealthChanged?.Invoke(_health);
        isInvincible = true;
        StartCoroutine(RemoveInvincibility());

        if (_health <= 0)
        {
            _animator.SetTrigger("DeathTrigger");
            foreach (GameObject go in _disableOnDeath)
            {
                go.SetActive(false);
            }

            StartCoroutine(DeleteAfterDeath());
        }
    }
    
    private IEnumerator RemoveInvincibility()
    {
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }

    private IEnumerator DeleteAfterDeath()
    {
        yield return new WaitForSeconds(0.4f);
        _model.SetActive(false);
    }
}