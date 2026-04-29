using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 30f;
    public float lifeTime = 3f;
    public float damage = 20f;

    public void Shoot(Vector3 direction, GameObject shooter)
    {
        // 1. ดึงทั้ง CharacterController และ Collider ปกติออกมา (ป้องกันไว้ก่อน)
        CharacterController shooterCC = shooter.GetComponent<CharacterController>();
        Collider shooterCol = shooter.GetComponent<Collider>();
        Collider bulletCol = GetComponent<Collider>();

        // 2. สั่งให้ Ignore ทั้งสองอย่าง
        if (bulletCol != null)
        {
            if (shooterCC != null) Physics.IgnoreCollision(bulletCol, shooterCC);
            if (shooterCol != null) Physics.IgnoreCollision(bulletCol, shooterCol);
        }

        GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // ถ้าชนศัตรู
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        // กระสุนผู้เล่นจะไม่เช็ค Tag "Player" เพื่อป้องกันความผิดพลาด
        // แต่จะทำลายตัวเองเมื่อชนสิ่งกีดขวางอื่นๆ
        Destroy(gameObject);
    }
}