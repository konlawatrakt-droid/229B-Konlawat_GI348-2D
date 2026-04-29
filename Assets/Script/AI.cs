using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform model;
    public Transform player;
    public float speed = 3f;
    public float waitTime = 1f;
    private EnemyDetection detection;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    private float nextFireTime;
    private Transform target;

    void Start()
    {
        detection = GetComponent<EnemyDetection>();
        target = pointA;
        StartCoroutine(MoveLoop());
    }


    IEnumerator MoveLoop()
    {
        while (true)
        {
            // เดินไป target
            while (Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            // หยุด
            yield return new WaitForSeconds(waitTime);

            // สลับเป้าหมาย
            target = (target == pointA) ? pointB : pointA;
        }
    }
    private void FixedUpdate()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;

        if (dir != Vector3.zero)
        {
            model.rotation = Quaternion.LookRotation(dir);
        }
    }


    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // คำนวณทิศทาง
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint = ray.GetPoint(100);
        if (Physics.Raycast(ray, out RaycastHit hit)) targetPoint = hit.point;
        Vector3 shootDir = (targetPoint - firePoint.position).normalized;

        // เรียกใช้สคริปต์ PlayerBullet และส่งค่าผู้เล่น (this.gameObject) เข้าไป
        PlayerBullet pBullet = bullet.GetComponent<PlayerBullet>();
        if (pBullet != null)
        {
            pBullet.Shoot(shootDir, gameObject);
        }
    }

    void Update()
    {
        if (detection.CanSeePlayer()) { Shoot(); }
    }
}

