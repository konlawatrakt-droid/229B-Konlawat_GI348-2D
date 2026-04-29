using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30; // จำนวนกระสุนที่จะได้รับ

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าสิ่งที่มาชนคือ "Player" หรือไม่
        if (other.CompareTag("Player"))
        {
            // ดึงสคริปต์ PlayerShooting ออกมาจากตัวผู้เล่น
            PlayerShooting shooting = other.GetComponent<PlayerShooting>();

            if (shooting != null)
            {
                // เรียกใช้ฟังก์ชัน AddAmmo ที่เราเขียนไว้ในสคริปต์ยิงปืน
                shooting.AddAmmo(ammoAmount);
                
                Debug.Log("เติมกระสุนแล้ว: +" + ammoAmount);

                // ทำลายกล่องกระสุนทิ้ง หรือจะใช้ SetActive(false) ก็ได้
                Destroy(gameObject);
            }
        }
    }
}