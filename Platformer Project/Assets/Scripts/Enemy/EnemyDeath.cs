using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Transform enemyParent;

    public void Die()
    {
        Destroy(enemyParent.gameObject);
    }
}
