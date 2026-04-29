using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;
    public float waitTime = 1f;

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private FieldOfView fov;
    private Transform currentTarget;
    private float nextFireTime;
    private bool isPatrolling = true;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
        currentTarget = pointA;
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (isPatrolling)
            {
                // เดินไปยังเป้าหมาย Patrol
                while (Vector3.Distance(transform.position, currentTarget.position) > 0.3f && isPatrolling)
                {
                    MoveTo(currentTarget.position);
                    yield return null;
                }

                if (isPatrolling)
                {
                    yield return new WaitForSeconds(waitTime);
                    currentTarget = (currentTarget == pointA) ? pointB : pointA;
                }
            }
            yield return null;
        }
    }

    void Update()
    {
        if (fov.visibleTarget != null)
        {
            // ถ้าเจอผู้เล่น: หยุดเดิน และโจมตี
            isPatrolling = false;
            AttackTarget(fov.visibleTarget);
        }
        else
        {
            // ถ้าไม่เจอ: กลับไปเดินลาดตระเวน
            isPatrolling = true;
        }
    }

    void MoveTo(Vector3 position)
    {
        Vector3 dir = (position - transform.position).normalized;
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 8f);
        }
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
    }

    void AttackTarget(Transform target)
    {
        // หันหน้าไปหาผู้เล่น
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);

        // ยิงตามอัตราที่กำหนด
        if (Time.time >= nextFireTime)
        {
            Shoot(target.position);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot(Vector3 targetPos)
    {
        Debug.Log("กำลังจะยิง!"); // ถ้าบรรทัดนี้ขึ้นใน Console แสดงว่า Logic AI ถูกต้องแล้ว
        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("สร้างกระสุนสำเร็จ");

            Vector3 shootDir = (targetPos - firePoint.position).normalized;
            bullet.GetComponent<Bullet>().Shoot(shootDir);
        }
        else
        {
            Debug.LogError("ลืมใส่ Prefab กระสุนหรือ FirePoint หรือเปล่า?");
        }
    }
}