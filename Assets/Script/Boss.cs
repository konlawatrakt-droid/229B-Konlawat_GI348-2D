using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject doorToOpen; // ลากประตูที่ต้องการให้เปิดมาใส่ช่องนี้

    public void OnBossDefeated()
    {
        Debug.Log("บอสตายแล้ว! กำลังเปิดประตู...");
        
        if (doorToOpen != null)
        {
            // วิธีเปิดประตู (เลือกแบบใดแบบหนึ่ง)
            doorToOpen.SetActive(false); // แบบหายไปเลย
            // หรือ doorToOpen.GetComponent<Animator>().SetTrigger("Open"); // แบบใช้ Animation
        }
    }
}