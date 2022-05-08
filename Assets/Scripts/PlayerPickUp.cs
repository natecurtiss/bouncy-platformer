using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Gizmos;
using static UnityEngine.Input;
using static UnityEngine.Physics2D;

class PlayerPickUp : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] KeyCode _pickUp;
    [SerializeField] KeyCode _drop;
    [SerializeField] UnityEvent<Pickup> _onPickUp;
    [SerializeField] UnityEvent<Pickup> _onCloseTo;
    [SerializeField] UnityEvent<Pickup> _onDrop;
    [SerializeField] LayerMask _layer;
    [SerializeField] Transform _holder;
    
    Pickup _active;

    void Update()
    {
        CheckPickUp();
        CheckDrop();
    }

    void OnDrawGizmos() => DrawWireSphere(transform.position, _radius);

    void CheckPickUp()
    {
        var hit = OverlapCircle(transform.position, _radius, _layer);
        if (hit is not null && hit.TryGetComponent(out Pickup pickup))
        {
            _onCloseTo.Invoke(pickup);
            if (GetKeyDown(_pickUp))
            {
                Throw();
                PickUp(pickup);
            }
        }
    }

    void CheckDrop()
    {
        if (GetKeyDown(_drop)) 
            Throw();
    }

    void Throw()
    {
        if (_active is not null)
        {
            var flip = transform.eulerAngles.y == 180 ? -1 : 1;
            var diagonal = new Vector2(flip, 1).normalized;
            _active.Throw(diagonal);
            _onDrop.Invoke(_active);
            _active = null;
        }
    }

    void PickUp(Pickup pickup)
    {
        pickup.PickUp(_holder);
        _active = pickup;
        _onPickUp.Invoke(_active);
    }
}
