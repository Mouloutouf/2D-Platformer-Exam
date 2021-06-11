using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CustomRigidbody pRb;

    public float speed;

    private float moveValue;
    public bool IsMoving => moveValue != 0.0f;
    public bool CanMove { get; set; }

    public Direction orientation { get; private set; }

    private void Start()
    {
        CanMove = true;
        orientation = Direction.Left;
    }

    public float CheckMoveInput()
    {
        return Input.GetAxis("Horizontal");
    }

    private void Update()
    {
        moveValue = CheckMoveInput();

        if (moveValue > 0.0f && orientation == Direction.Left)
        {
            orientation = Direction.Right;
        }
        if (moveValue < 0.0f && orientation == Direction.Right)
        {
            orientation = Direction.Left;
        }
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;

        Move(moveValue);
    }

    public void Move(float _value)
    {
        pRb.Velocity = new Vector2(_value * speed, pRb.Velocity.y);
    }
}
