using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winMenuUI; // ลาก Panel WinMenu มาใส่ที่นี่

    void OnTriggerEnter(Collider other)
    {
        // เช็คว่าผู้เล่นเป็นคนมาแตะใช่ไหม
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }

    void Win()
    {
        // 1. เปิดหน้าจอ Win Menu
        if (winMenuUI != null)
        {
            winMenuUI.SetActive(true);
        }

        // 2. หยุดเวลาในเกม
        Time.timeScale = 0f;

        // 3. ปลดล็อกเมาส์ให้กดปุ่มได้
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}