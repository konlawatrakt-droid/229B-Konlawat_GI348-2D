using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpBar;

    // ✅ เพิ่มตรงนี้
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hurtSound;  // เสียงตอนโดนตี
    public AudioClip dieSound;   // เสียงตอนตาย (optional)

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

        // ✅ เล่นเสียงเจ็บ
        if (audioSource != null && hurtSound != null)
            audioSource.PlayOneShot(hurtSound);

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

    public void ResetHP()
    {
        currentHP = maxHP;
        if (hpBar != null) hpBar.value = currentHP;
        Debug.Log("HP Reset to full!");
    }

    void Die()
    {
        // ✅ เล่นเสียงตาย
        if (audioSource != null && dieSound != null)
            audioSource.PlayOneShot(dieSound);

        currentHP = maxHP;
        hpBar.value = currentHP;
        respawn.Respawn();
    }
}