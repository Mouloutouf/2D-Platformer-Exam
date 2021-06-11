using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public CustomRigidbody pRb;

    public float speed;

    public float MoveDir { get; set; }
    public bool IsMoving => MoveDir != 0.0f;
    public bool CanMove { get; set; }

    public Direction startDirection;

    private void Start()
    {
        CanMove = true;
        MoveDir = startDirection == Direction.Left ? -1.0f : 1.0f;
    }

    private void Update()
    {
        if (!CanMove) return;

        Move(MoveDir);
    }

    public void Move(float _value)
    {
        pRb.Velocity = new Vector2(_value * speed, pRb.Velocity.y);
    }
}
