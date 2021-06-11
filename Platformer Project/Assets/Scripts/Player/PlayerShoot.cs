using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public PlayerMovement movement;

    public Transform bulletCache;

    public GameObject bulletPrefab;

    public float cooldown;
    private float currentCooldown;

    public bool IsShooting { get; private set; }
    public bool CanShoot { get; set; }

    public float AimDirection => movement.orientation == Direction.Right ? 1.0f : -1.0f;

    private void Start()
    {
        CanShoot = true;
    }

    public bool CheckShootInput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Mouse0);
    }

    private void Update()
    {
        IsShooting = CheckShootInput();

        if (IsShooting)
        {
            if (!CanShoot) return;

            if (currentCooldown <= 0.0f)
            {
                Shoot();
                currentCooldown = cooldown;
            }
        }
        currentCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletCache);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.moveDir = AimDirection;
    }
}
