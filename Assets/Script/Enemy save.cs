using UnityEngine;
using System.Collections.Generic;

public class EnemySaveManager : MonoBehaviour
{
    public static EnemySaveManager instance;

    [Header("Data Lists")]
    public List<string> permanentDeadIDs = new List<string>(); // ตายถาวร (หลังแตะ Checkpoint)
    public List<string> tempDeadIDs = new List<string>();      // ตายชั่วคราว (ระหว่างทาง)

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void MarkAsDead(string id)
    {
        if (!tempDeadIDs.Contains(id)) tempDeadIDs.Add(id);
    }

    // ยืนยันการตายถาวรเมื่อถึง Checkpoint
    public void CommitDeaths()
    {
        foreach (string id in tempDeadIDs)
        {
            if (!permanentDeadIDs.Contains(id)) permanentDeadIDs.Add(id);
        }
        tempDeadIDs.Clear();
        Debug.Log("ยืนยันการตายถาวรเรียบร้อย");
    }

    // คืนชีพศัตรูที่ตายระหว่างทาง
    public void ResetTempDeaths()
    {
        tempDeadIDs.Clear();

        // ค้นหาศัตรูทั้งหมดในฉากและสั่งให้เช็คตัวเอง
        EnemyHP[] allEnemies = Resources.FindObjectsOfTypeAll<EnemyHP>();
        foreach (EnemyHP enemy in allEnemies)
        {
            enemy.CheckAndRespawn();
        }
    }
}