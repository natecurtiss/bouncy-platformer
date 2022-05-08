﻿using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ForceMode2D;

abstract class Pickup : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField] float _throwForce;
    [SerializeField] UnityEvent _onPickedUp;
    [SerializeField] UnityEvent _onThrown;

    void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    public abstract void Use();

    public void PickUp(Transform holder)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.simulated = false;
        transform.SetParent(holder);
        transform.position = holder.position;
        transform.rotation = holder.rotation;
        _onPickedUp.Invoke();
    }

    public void Throw(Vector2 direction)
    {
        _rigidbody.simulated = true;
        _rigidbody.AddForce(direction * _throwForce, Impulse);
        _onThrown.Invoke();
    }
}