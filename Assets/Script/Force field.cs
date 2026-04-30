using UnityEngine;

public class ForceField : MonoBehaviour
{
    [Header("Shield Stats")]
    public int maxHits = 2;          // โดนกี่ทีแตก
    private int currentHits;

    public float knockbackForce = 10f;

    [Header("Protection Settings")]
    private float lastDamageTime;
    public float damageInterval = 0.3f; // ป้องกันนับ Hit ซ้ำในเสี้ยววินาที

    public bool IsBroken { get; private set; } = false;

    // ✅ เปลี่ยนจาก HP เป็น Hit ที่เหลือ (ให้ Controller ดึงไปแสดง UI)
    public int GetCurrentHits() => currentHits;

    void Awake()
    {
        currentHits = maxHits;
    }

    void OnEnable()
    {
        if (currentHits <= 0) currentHits = 1;
        IsBroken = false;
        Debug.Log("Force Field Active - Hits remaining: " + currentHits);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time < lastDamageTime + damageInterval) return;

        if (other.CompareTag("EnemyBullet"))
        {
            lastDamageTime = Time.time;
            TakeHit();
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            lastDamageTime = Time.time;
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                Vector3 direction = other.transform.position - transform.position;
                enemyRb.AddForce(direction.normalized * knockbackForce, ForceMode.Impulse);
            }
            TakeHit();
        }
    }

    void TakeHit()
    {
        currentHits--;
        Debug.Log("Shield Hit! Hits remaining: " + currentHits);

        if (currentHits <= 0)
        {
            currentHits = 0;
            IsBroken = true;
            Debug.Log("Shield Broken!");
            gameObject.SetActive(false);
        }
    }

    // เรียกตอน Regen เพิ่ม Hit กลับมา
    public void RepairHit(int amount = 1)
    {
        currentHits = Mathf.Min(currentHits + amount, maxHits);
    }
}