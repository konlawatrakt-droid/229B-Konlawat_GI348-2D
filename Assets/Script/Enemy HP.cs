using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public float maxHP = 50f;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log("ศัตรูโดนยิง! เลือดเหลือ: " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ใส่ Effect ระเบิดตรงนี้ได้
        Destroy(gameObject);
    }
}