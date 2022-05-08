using UnityEngine;
using static UnityEngine.Gizmos;
using static UnityEngine.Input;
using static UnityEngine.Mathf;
using static UnityEngine.Physics2D;
using static UnityEngine.Time;

class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField] float _speed;
    [SerializeField] float _acceleration;
    [SerializeField] float _deceleration;
    [SerializeField] float _jump;
    [SerializeField] float _groundDistance;
    [SerializeField] float _coyoteTime;

    float _horizontalInput;
    float _jumpBuffer;
    float _groundTimer;

    void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    void Update()
    {
        TickTimers();
        CheckGround();
        GetInput();
    }

    void FixedUpdate()
    {
        Flip();
        Move();
        if (_jumpBuffer > 0 && _groundTimer > 0)
            Jump();
    }

    void OnDrawGizmos() => DrawRay(transform.position, Vector3.down * _groundDistance);

    void TickTimers()
    {
        _groundTimer -= deltaTime;
        _jumpBuffer -= deltaTime;
    }

    void CheckGround()
    {
        var hit = Raycast(transform.position, Vector2.down, _groundDistance);
        if (hit.collider is not null)
            _groundTimer = _coyoteTime;
    }
    
    void GetInput()
    {
        _horizontalInput = GetAxisRaw("Horizontal");
        if (GetKeyDown(KeyCode.W) || GetKeyDown(KeyCode.UpArrow))
            _jumpBuffer = _coyoteTime;
    }

    void Flip()
    {
        var rot = _horizontalInput switch
        {
            1 => 0,
            -1 => 180,
            _ => transform.eulerAngles.y
        };
        transform.eulerAngles = new(transform.eulerAngles.x, rot, transform.eulerAngles.z);
    }

    void Move()
    {
        var target = _speed * _horizontalInput;
        var t = target == 0 ? _deceleration : _acceleration;
        var lerp = Lerp(_rigidbody.velocity.x, target, t);
        _rigidbody.velocity = new(lerp, _rigidbody.velocity.y);
    }

    void Jump()
    {
        _rigidbody.velocity = new(_rigidbody.velocity.x, _jump);
        _groundTimer = 0f;
        _jumpBuffer = 0f;
    }
}
