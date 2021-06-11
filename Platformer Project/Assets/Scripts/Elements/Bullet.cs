using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float moveDir { get; set; }

    public LayerMask enemyMask;
    public LayerMask obstacleMask;

    private void Update()
    {
        Move(moveDir);
    }

    public void Move(float _value)
    {
        Vector3 movement = Vector3.right * _value;
        transform.position += movement * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyMask == (enemyMask | (1 << collision.gameObject.layer)))
        {
            EnemyDeath enemyDeath = collision.GetComponentInParent<EnemyDeath>();
            enemyDeath.Die();

            Kill();
        }
        if (obstacleMask == (obstacleMask | (1 << collision.gameObject.layer)))
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
