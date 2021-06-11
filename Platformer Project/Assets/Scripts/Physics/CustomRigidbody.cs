using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    public Transform parentTransform;
    public Collider2D collider2D;

    public float mass;

    private EdgeRaycast edgeRaycast;

    public int castPrecision;
    public float castDistance;

    public LayerMask groundMask;

    private float upLimit, downLimit, leftLimit, rightLimit;
    public bool OnGround => downLimit != -Mathf.Infinity;
    public bool WallLeft => leftLimit != -Mathf.Infinity;
    public bool WallRight => rightLimit != Mathf.Infinity;
    public bool RoofUp => upLimit != Mathf.Infinity;

    public Vector2 Velocity { get; set; }

    private void Awake()
    {
        upLimit = rightLimit = Mathf.Infinity;
        downLimit = leftLimit = -Mathf.Infinity;

        edgeRaycast = new EdgeRaycast(collider2D, 0.2f);
    }

    private void FixedUpdate()
    {
        upLimit = rightLimit = Mathf.Infinity;
        downLimit = leftLimit = -Mathf.Infinity;

        RigidbodyCheck(Direction.Down);
        RigidbodyCheck(Direction.Up);
        RigidbodyCheck(Direction.Left);
        RigidbodyCheck(Direction.Right);

        Vector2 position = (Vector2)parentTransform.position + Velocity * Time.deltaTime;
        Vector3 clampPosition = new Vector2(Mathf.Clamp(position.x, leftLimit, rightLimit), Mathf.Clamp(position.y, downLimit, upLimit));
        parentTransform.position = clampPosition;
    }

    public void RigidbodyCheck(Direction direction)
    {
        edgeRaycast.EdgeCheck(castPrecision, direction, castDistance, groundMask, out RaycastHit2D hit);
        if (hit.collider != null)
        {
            float colliderExtent;
            float hitBound;
            switch (direction)
            {
                case Direction.Up:
                    hitBound = hit.collider.bounds.min.y; colliderExtent = -collider2D.bounds.extents.y;
                    upLimit = hitBound + colliderExtent;
                    Debug.LogWarning(upLimit);
                    break;
                case Direction.Down:
                    hitBound = hit.collider.bounds.max.y; colliderExtent = collider2D.bounds.extents.y;
                    downLimit = hitBound + colliderExtent;
                    Debug.LogWarning(downLimit);
                    break;
                case Direction.Left:
                    hitBound = hit.collider.bounds.max.x; colliderExtent = collider2D.bounds.extents.x;
                    leftLimit = hitBound + colliderExtent;
                    Debug.LogWarning(leftLimit);
                    break;
                case Direction.Right:
                    hitBound = hit.collider.bounds.min.x; colliderExtent = -collider2D.bounds.extents.x;
                    rightLimit = hitBound + colliderExtent;
                    Debug.LogWarning(rightLimit);
                    break;
                default:
                    break;
            }
        }
        edgeRaycast.DisplayCastDebug(Color.yellow);
    }
}
