using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 30f;
    private float currentHP;
    public bool isBoss = false; // ติ๊กช่องนี้เฉพาะตัวที่เป็นบอส

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        if (isBoss)
        {
            // แจ้ง Manager ว่าบอสตายแล้ว
            BossManager manager = FindFirstObjectByType<BossManager>();
            if (manager != null) manager.OnBossDefeated();
        }
        Destroy(gameObject);
    }
}