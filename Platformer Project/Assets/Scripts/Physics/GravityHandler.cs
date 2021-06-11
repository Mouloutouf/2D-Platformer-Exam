using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour
{
    public CustomRigidbody pRb;
    public Gravity gravity; private Vector2 Gravity => Vector2.up * gravity.value;

    private void FixedUpdate()
    {
        if (pRb.OnGround) return;

        float gravityFraction = (Gravity.y * pRb.mass) / 100;
        pRb.Velocity = new Vector2(pRb.Velocity.x, pRb.Velocity.y + gravityFraction);
    }
}
