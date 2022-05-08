using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Time;

class PlayerCombat : MonoBehaviour
{
    [SerializeField] float _cooldown;
    Pickup _weapon;
    float _cooldownTimer;

    void Update()
    {
        if (GetMouseButtonDown(0) && _weapon is not null && _cooldownTimer <= 0)
        {
            _weapon.Use();
            _cooldownTimer = _cooldown;
        }
        _cooldownTimer -= deltaTime;
    }

    public void ChangeWeapon(Pickup pickup) => _weapon = pickup;

    public void RemoveWeapon() => _weapon = null;
}