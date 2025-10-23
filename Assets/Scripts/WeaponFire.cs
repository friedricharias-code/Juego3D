using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFire : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;

    private float nextFireTime = 0f;

    // Este método lo llamaremos desde el evento del Player Input
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed) // solo cuando se presiona
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        if (bulletPrefab && muzzlePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb)
                rb.velocity = muzzlePoint.forward * bulletSpeed;
        }
    }
}

