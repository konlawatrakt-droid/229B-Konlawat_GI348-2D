using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpBar; // 🔥 ลาก UI มาใส่

    private PlayerRespawn respawn;

    void Start()
    {
        currentHP = maxHP;
        respawn = GetComponent<PlayerRespawn>();

        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;

        hpBar.value = currentHP; // 🔥 อัปเดตหลอดเลือด

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;

        if (currentHP > maxHP)
            currentHP = maxHP;

        // อัปเดต UI
        if (hpBar != null)
            hpBar.value = currentHP;
    }



    void Die()
    {
        currentHP = maxHP;
        hpBar.value = currentHP; // 🔥 รีหลอด

        respawn.Respawn();
    }
}