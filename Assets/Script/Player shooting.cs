using UnityEngine;
using UnityEngine.UI; // สำหรับแสดงจำนวนกระสุนบน UI

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxAmmo = 30;
    public int currentAmmo;
    
    public Text ammoText; // ลาก UI Text มาใส่เพื่อดูจำนวนกระสุน

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // คลิกซ้ายเพื่อยิง
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("กระสุนหมด!");
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoUI();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // ยิงไปข้างหน้าตามทิศทางของกล้องหรือตัวผู้เล่น
        bullet.GetComponent<Bullet>().Shoot(firePoint.forward);
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