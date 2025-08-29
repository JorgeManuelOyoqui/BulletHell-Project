using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;
    private Camera mainCamera;
    private PlayerControls controls;
    private bool isShooting;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += ctx => isShooting = true;
        controls.Gameplay.Shoot.canceled += ctx => isShooting = false;
    }

    void OnEnable() => controls.Gameplay.Enable();
    void OnDisable() => controls.Gameplay.Disable();

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = (mousePos - transform.position).normalized;
        direction.z = 0;

        GameObject bullet = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<PlayerBullet>().direction = direction;
        BulletManager.Instance.AddBullet(bullet);
    }
}