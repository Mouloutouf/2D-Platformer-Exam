using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public CustomRigidbody pRb;

    public EnemyMovement enemyMovement;

    private bool stuckLeft, stuckRight;

    private void FixedUpdate()
    {
        if (pRb.WallLeft && !stuckLeft)
        {
            enemyMovement.MoveDir *= -1;
            stuckLeft = true;
            stuckRight = false;
        }
        if (pRb.WallRight && !stuckRight)
        {
            enemyMovement.MoveDir *= -1;
            stuckRight = true;
            stuckLeft = false;
        }
    }
}
