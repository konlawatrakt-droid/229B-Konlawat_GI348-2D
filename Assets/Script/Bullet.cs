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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForceField"))
        {
            // ถ้าชนโล่ ให้กระสุนหายไปเฉยๆ ไม่ต้องลดเลือดผู้เล่น
            Destroy(gameObject);
            return;
        }
        if (other.CompareTag("Player"))
        {
            // ลดเลือดผู้เล่น
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // ถ้าชนผู้เล่น
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHP hp = collision.gameObject.GetComponent<PlayerHP>();
            if (hp != null) hp.TakeDamage(10f);
        }
        // ถ้าชนศัตรู
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemyHp = collision.gameObject.GetComponent<EnemyHP>();
            if (enemyHp != null) enemyHp.TakeDamage(20f);
        }

        Destroy(gameObject);
    }
}