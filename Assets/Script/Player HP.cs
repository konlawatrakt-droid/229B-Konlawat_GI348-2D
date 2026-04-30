using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpBar;

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
        hpBar.value = currentHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
        if (hpBar != null) hpBar.value = currentHP;
    }

    // ✅ เพิ่มตรงนี้ — PlayerRespawn จะเรียกตอน Respawn
    public void ResetHP()
    {
        currentHP = maxHP;
        if (hpBar != null) hpBar.value = currentHP;
        Debug.Log("HP Reset to full!");
    }

    void Die()
    {
        currentHP = maxHP;
        hpBar.value = currentHP;
        respawn.Respawn();
    }
}