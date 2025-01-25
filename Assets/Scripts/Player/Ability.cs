using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Ability : MonoBehaviour
{
    [SerializeField] private ColliderType _colliderType;
    [SerializeField] private SoAbillity _abillityData;

    public SoAbillity AbilityData => _abillityData;

    [SerializeField] private TriggerCollider _c;

    [HideInInspector] public List<Enemy> _hitEnemysWithLastCast = new();

    private float _currentCoolDown;

    public float CurrentCooldown => _currentCoolDown;

    public event Action<Ability> Finished;

    private void Update()
    {
        _currentCoolDown -= Time.deltaTime;
    }

    public bool TryCast()
    {
        if (_currentCoolDown > 0f)
        {
            // Ability on cooldown.
            Debug.Log("Abililty on CD");
            return false;
        }
        _currentCoolDown = _abillityData.Cooldown + (_abillityData.AnimationLength) / _abillityData.AnimationSpeed;
        if (_abillityData.CastTime > 0f)
        {
            StartCoroutine(CastAfterSeconds(_abillityData.CastTime));
        }
        else
        {
            ShootAbility();
        }
        return true;
    }
    
    private IEnumerator CastAfterSeconds(float afterSeconds)
    {
        yield return new WaitForSeconds(afterSeconds / _abillityData.AnimationSpeed);

        ShootAbility();
    }

    private void ShootAbility()
    {
        _hitEnemysWithLastCast.Clear();
        switch (_colliderType)
        {
            case ColliderType.Sphere:
                _c.Collider.enabled = true;

                if (_c.Collider is SphereCollider s)
                {
                    DOTween.To(() => s.radius, x => s.radius = x, _abillityData.Range,
                        _abillityData.TimeToReachMaxRange).OnComplete(() =>
                    {
                        s.radius = 0;
                        s.enabled = false;
                        Finished?.Invoke(this);
                    });
                }

                break;
            case ColliderType.Box:
                _c.Collider.enabled = true;
                if (_c.Collider is BoxCollider b)
                {
                    b.transform.DOLocalRotate(new Vector3(0, -180f, 0), _abillityData.TimeToReachMaxRange/ _abillityData.AnimationSpeed,
                        RotateMode.LocalAxisAdd).OnComplete(() =>
                    {
                        _c.Collider.enabled = false;
                        b.transform.localRotation = Quaternion.identity;
                        Finished?.Invoke(this);
                    });
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }
}

enum ColliderType
{
    Sphere = 0,
    Box = 1,
}