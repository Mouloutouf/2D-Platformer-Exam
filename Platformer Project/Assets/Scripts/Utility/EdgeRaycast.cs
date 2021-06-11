using System;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }
public enum Axis { X, Y }

public class EdgeRaycast
{
    public Collider2D collider;
    public Bounds Bounds => collider.bounds;
    private Vector2[] castPositions; public Vector2[] CastPositions => castPositions;

    private Vector2 castDirection;
    private Axis mainAxis;

    private float offset;

    public EdgeRaycast(Collider2D _collider, float _offset)
    {
        collider = _collider;
        offset = _offset;
    }

    public Vector2 GetCastDirection(Direction _direction)
    {
        switch (_direction)
        {
            case Direction.Up: return Vector2.up;
            case Direction.Down: return Vector2.down;
            case Direction.Left: return Vector2.left;
            case Direction.Right: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    public bool EdgeCheck(int _precision, Direction _direction, float _distance, LayerMask _layerMask, out RaycastHit2D hit)
    {
        InitializeEdge(_direction, _distance);

        CreateEdgePositions(_precision, _direction);

        return RaycastCheck(_precision, _distance, _layerMask, out hit);
    }

    public void InitializeEdge(Direction _direction, float _distance)
    {
        castDirection = GetCastDirection(_direction);
        mainAxis = _direction == Direction.Up || _direction == Direction.Down ? Axis.Y : Axis.X;

        castDistance = _distance;
    }

    public bool RaycastCheck(int _precision, float _distance, LayerMask _layerMask, out RaycastHit2D hit)
    {
        hit = new RaycastHit2D();

        RaycastHit2D[] hits = new RaycastHit2D[_precision];

        for (int i = 0; i < castPositions.Length; i++)
        {
            hits[i] = Physics2D.Raycast(castPositions[i], castDirection, _distance, _layerMask);
            if (hits[i])
            {
                Debug.Log("Hit");
                hit = hits[i];
                return true;
            }
        }
        return false;
    }

    public void CreateEdgePositions(int _precision, Direction _direction)
    {
        castPositions = new Vector2[_precision];
        float refSize = (mainAxis == Axis.Y ? Bounds.size.x : Bounds.size.y) - (offset * 2);
        float boundOffset = refSize / (_precision - 1);
        float currentOffset = offset;

        float refMin = mainAxis == Axis.Y ? Bounds.min.x : Bounds.min.y;
        float edge =
            _direction == Direction.Up ? Bounds.center.y + Bounds.extents.y :
            _direction == Direction.Down ? Bounds.center.y - Bounds.extents.y :
            _direction == Direction.Left ? Bounds.center.x - Bounds.extents.x :
            _direction == Direction.Right ? Bounds.center.x + Bounds.extents.x : 0.0f;
        for (int i = 0; i < castPositions.Length; i++)
        {
            Vector2 castPos = mainAxis == Axis.Y ? new Vector2(refMin + currentOffset, edge) : new Vector2(edge, refMin + currentOffset);
            castPositions[i] = castPos;
            currentOffset += boundOffset;
        }
    }

    private float castDistance;
    public void DisplayCastGizmos(Color _color)
    {
        if (castPositions == null || castPositions.Length == 0) return;

        Gizmos.color = _color;
        for (int i = 0; i < castPositions.Length; i++)
        {
            Gizmos.DrawRay(castPositions[i], castDirection * castDistance);
        }
    }
    public void DisplayCastDebug(Color _color)
    {
        if (castPositions == null || castPositions.Length == 0) return;

        for (int i = 0; i < castPositions.Length; i++)
        {
            Debug.DrawRay(castPositions[i], castDirection * castDistance, _color);
        }
    }
}
