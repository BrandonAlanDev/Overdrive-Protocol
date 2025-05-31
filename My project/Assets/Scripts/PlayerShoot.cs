using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootCooldown = 1f;

    private float lastShootTime = -Mathf.Infinity;
    private Camera mainCamera;
    private PlayerInputActions input;
    private bool isShooting = false;

    void Awake()
    {
        mainCamera = Camera.main;
        input = new PlayerInputActions();
        input.Player.Enable();

        input.Player.Shoot.performed += ctx => isShooting = true;
        input.Player.Shoot.canceled += ctx => isShooting = false;
    }

    void Update()
    {
        if (!isShooting) return;

        if (Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 dir = (hit.point - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(dir));
            bullet.GetComponent<Bullet>().Init(dir);
        }
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
