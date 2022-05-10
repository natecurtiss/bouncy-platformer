using UnityEngine;

class Stick : Pickup
{
    public override void Use(out float cooldown)
    {
        Debug.Log("Pow");
        cooldown = 0.1f;
    }
}