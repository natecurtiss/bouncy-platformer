using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ForceMode2D;

abstract class Pickup : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField] float _throwForce;
    [SerializeField] UnityEvent _onPickedUp;
    [SerializeField] UnityEvent _onThrown;
    
    public Transform LeftHand { get; set; }
    public Transform RightHand { get; set; }
    protected bool IsPickedUp { get; private set; }

    protected virtual void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    public abstract void Use(out float cooldown);

    public void PickUp(Transform holder)
    {
        _rigidbody.simulated = false;
        transform.SetParent(holder);
        transform.position = holder.position;
        transform.rotation = holder.rotation;
        _onPickedUp.Invoke();
        IsPickedUp = true;
    }

    public void Throw(Vector2 direction)
    {
        transform.SetParent(null);
        _rigidbody.simulated = true;
        _rigidbody.AddForce(direction * _throwForce, Impulse);
        _onThrown.Invoke();
        IsPickedUp = false;
    }
}