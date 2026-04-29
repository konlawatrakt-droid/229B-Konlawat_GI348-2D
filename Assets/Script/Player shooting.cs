using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxAmmo = 30;
    public int currentAmmo;

    [Header("Firing Settings")]
    public float fireRate = 5f; // ยิงได้กี่นัดต่อวินาที (เช่น 5 นัด/วินาที)
    private float nextFireTime = 0f;

    public Text ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        // เปลี่ยนจาก GetButton เป็น GetButtonDown ถ้าอยากให้กดหนึ่งครั้งยิงหนึ่งนัด
        // หรือใช้ GetButton ค้างไว้แต่ต้องเช็ค Time.time ให้แม่นยำ
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                nextFireTime = Time.time + (1f / fireRate); // 🔥 คำนวณเวลานัดถัดไปก่อนยิง
                Shoot();
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // ✅ ใช้ทิศหน้าของ firePoint โดยตรง ไม่ยุ่งกับกล้องเลย
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