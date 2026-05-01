using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpBar;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hurtSound;
    public AudioClip dieSound;

    [Header("Sound Cooldown")]
    public float hurtSoundCooldown = 0.5f; // เล่นเสียงซ้ำได้ทุกกี่วินาที
    private float lastHurtSoundTime = -999f; // ✅ ตั้งให้เล่นได้ทันทีตอนแรก

    private PlayerRespawn respawn;

    void Start()
    {
        currentHP = maxHP;
        respawn = GetComponent<PlayerRespawn>();

        hpBar.maxValue = maxHP;
        hpBar.value = currentHP;

        Time.timeScale = 1f; // รีเซ็ตเวลาทุกครั้งที่เริ่มซีนใหม่

        // ถ้าอยากให้เมาส์หายไปตอนเริ่มเล่นเกมด้วย
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        hpBar.value = currentHP;

        // ✅ เช็ค Cooldown ก่อนเล่นเสียง
        if (audioSource != null && hurtSound != null)
        {
            if (Time.time >= lastHurtSoundTime + hurtSoundCooldown)
            {
                audioSource.PlayOneShot(hurtSound);
                lastHurtSoundTime = Time.time;
            }
        }

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
        lastHurtSoundTime = -999f; // ✅ Reset cooldown ด้วยตอน Respawn
        Debug.Log("HP Reset to full!");
    }

    void Die()
    {
        if (audioSource != null && dieSound != null)
            audioSource.PlayOneShot(dieSound);

        currentHP = maxHP;
        hpBar.value = currentHP;
        respawn.Respawn();
    }
}