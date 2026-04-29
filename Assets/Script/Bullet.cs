using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public float damage = 10f; // 👈 เพิ่มพลังโจมตีของกระสุน

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // เช็คว่าชนโดน Object ที่มี Tag ว่า "Player" หรือไม่
        if (collision.gameObject.CompareTag("Player"))
        {
            // ดึงสคริปต์ PlayerHP จากตัวผู้เล่นที่ถูกชน
            PlayerHP playerHealth = collision.gameObject.GetComponent<PlayerHP>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // 👈 ส่งค่าความเสียหายไปลด HP
                Debug.Log("ผู้เล่นโดนยิง! ลดเลือดไป: " + damage);
            }
        }

        Destroy(gameObject);
    }
}