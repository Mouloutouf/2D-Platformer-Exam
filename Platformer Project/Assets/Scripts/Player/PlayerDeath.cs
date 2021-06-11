using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Transform deathCanvas;

    public PlayerMovement movement;
    public PlayerJump jump;

    public LayerMask damageMask;
    public LayerMask enemyMask;

    private void Start()
    {
        deathCanvas.gameObject.SetActive(false);
    }

    public void Die()
    {
        movement.CanMove = false;
        jump.CanJump = false;

        deathCanvas.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageMask == (damageMask | (1 << collision.gameObject.layer))) Die();

        if (enemyMask == (enemyMask | (1 << collision.gameObject.layer))) Die();
    }
}
