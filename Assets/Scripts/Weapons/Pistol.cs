using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.ForceMode2D;
using static UnityEngine.Input;
using static UnityEngine.Mathf;
using static UnityEngine.Quaternion;

class Pistol : Pickup
{
    Camera _camera;
    
    [SerializeField] Rigidbody2D _bullet;
    [SerializeField] Transform _tip;
    [SerializeField] float _power;
    [SerializeField] float _cooldown;
    [SerializeField] UnityEvent _onShoot;

    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }

    void Update()
    {
        if (IsPickedUp) 
            RotateToMouse();
    }

    public override void Use(out float cooldown)
    {
        var bullet = Instantiate(_bullet, _tip.position, transform.rotation);
        bullet.AddForce(transform.right * _power, Impulse);
        _onShoot.Invoke();
        cooldown = _cooldown;
    }

    void RotateToMouse()
    {
        var screenPos = new Vector3(mousePosition.x, mousePosition.y, _camera.transform.position.z);
        var mousePos = _camera.ScreenToWorldPoint(screenPos);
        var y = mousePos.y - transform.position.y;
        var x = mousePos.x - transform.position.x;
        var angle = Atan2(y, x) * Rad2Deg;
        transform.rotation = AngleAxis(angle, Vector3.forward);
    }
}
