using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public CustomRigidbody pRb;

    public float force;

    public bool IsJumping { get; private set; }
    public bool CanJump { get; set; }

    public int jumpAmount;
    private int currentJumpCount;

    private void Start()
    {
        CanJump = true;
    }

    public bool CheckJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space);
    }

    private void Update()
    {
        if (!IsJumping)
        {
            IsJumping = CheckJumpInput();
        }
    }

    private void FixedUpdate()
    {
        if (IsJumping)
        {
            IsJumping = false;

            if (!CanJump) return;

            if (pRb.OnGround)
            {
                currentJumpCount = 0;
            }

            if (currentJumpCount >= jumpAmount) return;

            Jump(force);
            currentJumpCount++;
        }
    }

    public void Jump(float _force)
    {
        pRb.Velocity = new Vector2(pRb.Velocity.x, _force);
    }
}
