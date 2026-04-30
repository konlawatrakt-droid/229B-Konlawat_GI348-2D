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

    [Header("Audio")]
    public AudioSource enemyAudioSource; // ลาก AudioSource ของศัตรูมาใส่
    public AudioClip shootSound;        // ลากไฟล์เสียงยิงมาใส่

    private FieldOfView fov;
    private Transform currentTarget;
    private float nextFireTime;
    private bool isPatrolling = true;

    void Start()
    {
        fov = GetComponent<FieldOfView>();
        currentTarget = pointA;

        // ถ้าลืมลาก AudioSource ใน Inspector ให้มันหาเองในเบื้องต้น
        if (enemyAudioSource == null)
            enemyAudioSource = GetComponent<AudioSource>();

        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (isPatrolling)
            {
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
        if (fov == null) return;

        if (fov.visibleTarget != null)
        {
            isPatrolling = false;
            AttackTarget(fov.visibleTarget);
        }
        else
        {
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
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);

        if (Time.time >= nextFireTime)
        {
            Shoot(target.position);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot(Vector3 targetPos)
    {
        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // --- ส่วนที่เพิ่ม: เล่นเสียงยิง ---
            if (enemyAudioSource != null && shootSound != null)
            {
                // สุ่ม Pitch เล็กน้อยเพื่อให้เสียงไม่น่าเบื่อ
                enemyAudioSource.pitch = Random.Range(0.9f, 1.1f);
                enemyAudioSource.PlayOneShot(shootSound);
            }
            // ---------------------------

            Vector3 shootDir = (targetPos - firePoint.position).normalized;
            bullet.GetComponent<Bullet>().Shoot(shootDir);
        }
        else
        {
            Debug.LogError("ลืมใส่ Prefab กระสุนหรือ FirePoint หรือเปล่า?");
        }
    }
}