using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerCollider : MonoBehaviour
{
    private Collider _collider;

    public Collider Collider => _collider;

    public event Action<Collider> TriggerEntered;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEntered?.Invoke(other);
    }
}