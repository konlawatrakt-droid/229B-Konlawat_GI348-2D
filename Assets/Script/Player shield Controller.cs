using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldController : MonoBehaviour
{
    [Header("Shield Object")]
    public GameObject forceFieldObject;

    [Header("Cooldown Settings")]
    public float brokenCooldown = 5f;
    private float cooldownTimer = 0f;
    private bool isOnCooldown = false;

    [Header("Regen Settings")]
    public float regenInterval = 3f; // ฟื้น 1 Hit ทุกกี่วินาทีตอนปิดโล่
    private float regenTimer = 0f;

    [Header("Shield UI")]
    public Slider shieldBar;
    public Text shieldText;
    public Text cooldownText;

    [Header("Audio Sources")]
    public AudioSource uiAudioSource;    // สำหรับเล่นเสียง UI/Shield

    [Header("Audio Clips")]
    public AudioClip shieldOpenSound;   // เสียงเปิดโล่
    public AudioClip shieldCloseSound;  // เสียงปิดโล่
    public AudioClip shieldBrokenSound; // เสียงโล่แตก

    [Header("Audio Cooldown")]
    public float audioCooldown = 0.3f;  // ป้องกันเสียงดังรัวเกินไป (กี่วินาทีถึงจะเล่นเสียงได้อีกครั้ง)
    private float lastAudioTime;

    private ForceField forceFieldScript;
    private bool isShieldActive = false;

    void Start()
    {
        if (forceFieldObject != null)
        {
            forceFieldScript = forceFieldObject.GetComponent<ForceField>();
            forceFieldObject.SetActive(false);
        }
        UpdateShieldUI();
    }

    void Update()
    {
        HandleCooldown();
        HandleRegen();

        if (Input.GetKeyDown(KeyCode.Q) && !isOnCooldown)
        {
            ToggleShield();
        }

        // ตรวจว่าโล่แตกเอง (Hit หมด)
        if (isShieldActive && forceFieldObject != null && !forceFieldObject.activeSelf)
        {
            isShieldActive = false;

            if (forceFieldScript != null && forceFieldScript.IsBroken)
            {
                StartCooldown();
            }
        }

        UpdateShieldUI();
    }

    void ToggleShield()
    {
        if (forceFieldObject == null) return;

        isShieldActive = !isShieldActive;
        forceFieldObject.SetActive(isShieldActive);

        // --- ระบบเล่นเสียงพร้อม Cooldown ---
        if (uiAudioSource != null && Time.time >= lastAudioTime + audioCooldown)
        {
            if (isShieldActive && shieldOpenSound != null)
            {
                uiAudioSource.PlayOneShot(shieldOpenSound);
                lastAudioTime = Time.time; // บันทึกเวลาที่เล่นเสียงล่าสุด
            }
            else if (!isShieldActive && shieldCloseSound != null)
            {
                uiAudioSource.PlayOneShot(shieldCloseSound);
                lastAudioTime = Time.time; // บันทึกเวลาที่เล่นเสียงล่าสุด
            }
        }

        regenTimer = 0f;
        Debug.Log(isShieldActive ? "โล่เปิด!" : "โล่ปิด!");
    }

    void HandleRegen()
    {
        if (!isShieldActive && !isOnCooldown && forceFieldScript != null)
        {
            if (forceFieldScript.GetCurrentHits() < forceFieldScript.maxHits)
            {
                regenTimer += Time.deltaTime;
                if (regenTimer >= regenInterval)
                {
                    forceFieldScript.RepairHit(1);
                    regenTimer = 0f;
                    Debug.Log("Shield regenerated 1 hit!");
                }
            }
            else
            {
                regenTimer = 0f;
            }
        }
    }

    void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = brokenCooldown;
        regenTimer = 0f;

        // เสียงโล่แตกให้เล่นทันที (ไม่ต้องเช็ค Audio Cooldown เพราะเหตุการณ์สำคัญ)
        if (uiAudioSource != null && shieldBrokenSound != null)
        {
            uiAudioSource.PlayOneShot(shieldBrokenSound);
        }

        Debug.Log("โล่แตก! เริ่มคูลดาวน์");
    }

    void HandleCooldown()
    {
        if (!isOnCooldown) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            isOnCooldown = false;
            cooldownTimer = 0f;
            Debug.Log("โล่พร้อมใช้งานอีกครั้ง!");
        }
    }

    void UpdateShieldUI()
    {
        if (forceFieldScript == null) return;

        int hits = forceFieldScript.GetCurrentHits();
        int maxHits = forceFieldScript.maxHits;

        if (shieldBar != null)
            shieldBar.value = (float)hits / maxHits;

        if (shieldText != null)
            shieldText.text = "Shield: " + hits + "/" + maxHits + " hits";

        if (cooldownText != null)
        {
            cooldownText.text = isOnCooldown
                ? "Shield Cooldown: " + cooldownTimer.ToString("F1") + "s"
                : "";
        }
    }
}