using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Abillity : MonoBehaviour
{
    [SerializeField] private ColliderType _colliderType;
    [SerializeField] private SoAbillity _abillityData;

    public SoAbillity AbilityData => _abillityData;

    [SerializeField] private TriggerCollider _c;

    [HideInInspector] public List<Enemy> _hitEnemysWithLastCast = new();

    private void Start()
    {
    }

    public void Cast()
    {
        // Add Cast Time
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
                    });
                }

                break;
            case ColliderType.Box:
                _c.Collider.enabled = true;
                if (_c.Collider is BoxCollider b)
                {
                    b.transform.DOLocalRotate(new Vector3(0, -180f, 0), _abillityData.TimeToReachMaxRange / 2.5f,
                        RotateMode.LocalAxisAdd).OnComplete(() => { _c.Collider.enabled = false;
                        b.transform.localRotation = Quaternion.identity;
                    });
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _hitEnemysWithLastCast.Clear();
        if (_abillityData.UseAnimatedCollider)
        {
            _c.Collider.enabled = true;
            StartCoroutine(DeactivateCollider(_abillityData.TimeToReachMaxRange));
            return;
        }
    }

    private IEnumerator DeactivateCollider(float afterSeconds)
    {
        yield return new WaitForSeconds(afterSeconds);

        _c.Collider.enabled = false;
    }
}

enum ColliderType
{
    Sphere = 0,
    Box = 1,
}