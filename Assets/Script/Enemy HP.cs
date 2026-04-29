using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Enemy Settings")]
    public string enemyID;
    public bool isBoss = false; // 👈 เพิ่มส่วนนี้กลับมาตามที่คุณต้องการ
    public float maxHP = 50f;

    private float currentHP;
    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        currentHP = maxHP;
        startPos = transform.position;
        startRot = transform.rotation;

        if (string.IsNullOrEmpty(enemyID))
        {
            Debug.LogError(gameObject.name + " ลืมใส่ Enemy ID ใน Inspector!");
        }
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log(enemyID + " โดนโจมตี! เลือดเหลือ: " + currentHP);
        if (currentHP <= 0) Die();
    }

    void Die()
    {
        if (EnemySaveManager.instance != null)
        {
            // 🔥 ตรรกะพิเศษ: ถ้าเป็นบอส ให้บันทึกว่าตายถาวร (Permanent) ทันที
            if (isBoss)
            {
                if (!EnemySaveManager.instance.permanentDeadIDs.Contains(enemyID))
                {
                    EnemySaveManager.instance.permanentDeadIDs.Add(enemyID);
                }
            }
            else
            {
                // ถ้าเป็นศัตรูทั่วไป ให้บันทึกแค่ตายชั่วคราว
                EnemySaveManager.instance.MarkAsDead(enemyID);
            }
        }

        gameObject.SetActive(false);
    }

    public void CheckAndRespawn()
    {
        if (EnemySaveManager.instance != null)
        {
            // ถ้าไม่อยู่ในบัญชีตายถาวร ให้กลับมาเกิดใหม่
            if (!EnemySaveManager.instance.permanentDeadIDs.Contains(enemyID))
            {
                gameObject.SetActive(true);
                currentHP = maxHP;
                transform.position = startPos;
                transform.rotation = startRot;
            }
        }
    }

    public void ResetHP()
    {
        currentHP = maxHP;
    }
}