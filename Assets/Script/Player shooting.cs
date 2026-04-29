using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxAmmo = 120;
    public int currentAmmo;

    [Header("Firing Settings")]
    public float fireRate = 5f;
    private float nextFireTime = 0f;

    public Text ammoText;

    [Header("Audio")]
    public AudioSource shootSource;
    public AudioClip shootSound;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                nextFireTime = Time.time + (1f / fireRate);
                Shoot();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        // 🔥 เพิ่มการเล่นเสียงที่นี่
        if (shootSource != null && shootSound != null)
        {
            shootSource.PlayOneShot(shootSound);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector3 shootDir = firePoint.forward;

        PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
        if (pBullet != null)
        {
            pBullet.Shoot(shootDir, gameObject);
        }
    }

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo > maxAmmo) currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
    }
}