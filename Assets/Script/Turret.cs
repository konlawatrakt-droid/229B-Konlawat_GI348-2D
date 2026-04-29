using UnityEngine;

public class StationaryEnemy : MonoBehaviour
{
    [Header("Detection")]
    private FieldOfView fov; // ใช้ FOV เดิมที่คุณมี
    public Transform model;   // ส่วนหัวหรือตัวที่จะให้หันตามผู้เล่น

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
    }

    void Update()
    {
        // 1. เช็คว่าเจอผู้เล่นไหมผ่าน FieldOfView
        if (fov != null && fov.visibleTarget != null)
        {
            LookAtTarget(fov.visibleTarget);
            
            // 2. ยิงเมื่อถึงเวลา
            if (Time.time >= nextFireTime)
            {
                Shoot(fov.visibleTarget);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void LookAtTarget(Transform target)
    {
        // หันโมเดลไปหาผู้เล่น (เฉพาะแกน Y เพื่อไม่ให้ตัวเอียงก้มเงย)
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            model.rotation = Quaternion.Slerp(model.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
        }
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            
            // คำนวณทิศทางจากปากกระบอกพุ่งไปหาตัวผู้เล่น
            Vector3 shootDir = (target.position - firePoint.position).normalized;
            
            // เรียกใช้สคริปต์ Bullet เดิมที่คุณมี
            bullet.GetComponent<Bullet>().Shoot(shootDir);
        }
    }
}