using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyID;
    public bool isBoss = false;
    public float maxHP = 50f;

    [Header("Audio Settings")]
    public AudioSource enemyAudioSource;
    public AudioClip hitSound;

    private float currentHP;
    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        currentHP = maxHP;
        startPos = transform.position;
        startRot = transform.rotation;

        if (enemyAudioSource == null)
            enemyAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;

        if (enemyAudioSource != null && hitSound != null)
        {
            enemyAudioSource.pitch = Random.Range(0.85f, 1.15f);
            enemyAudioSource.PlayOneShot(hitSound);
        }

        if (currentHP <= 0) Die();
    }

    void Die()
    {
        if (EnemySaveManager.instance != null)
        {
            if (isBoss)
            {
                if (!EnemySaveManager.instance.permanentDeadIDs.Contains(enemyID))
                {
                    EnemySaveManager.instance.permanentDeadIDs.Add(enemyID);
                }
            }
            else
            {
                EnemySaveManager.instance.MarkAsDead(enemyID);
            }
        }
        gameObject.SetActive(false);
    }

    public void CheckAndRespawn()
    {
        if (EnemySaveManager.instance != null)
        {
            if (!EnemySaveManager.instance.permanentDeadIDs.Contains(enemyID))
            {
                gameObject.SetActive(true);
                currentHP = maxHP;
                transform.position = startPos;
                transform.rotation = startRot;
            }
        }
    }
}