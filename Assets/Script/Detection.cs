using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform player;
    public float viewDistance = 10f;
    public float viewAngle = 90f; // ย้ายมาไว้บนด้วย
    public LayerMask obstacleMask;

    private bool playerInRange = false;

    // ลบ Update() เก่าทิ้ง แล้วใช้ฟังก์ชันนี้แทน
    public bool CanSeePlayer()
    {
        if (!playerInRange)
        {
            // ถ้าบรรทัดนี้ขึ้น แสดงว่า Trigger ไม่ทำงาน หรือยังไม่เดินเข้าระยะ
            // Debug.Log("ผู้เล่นยังไม่อยู่ในระยะ Trigger"); 
            return false;
        }

        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (angle < viewAngle / 2f)
        {
            if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, viewDistance, obstacleMask))
            {
                // ดูว่า Raycast ไปชนกับ Object ชื่ออะไร
                Debug.Log("Raycast ชนกับ: " + hit.transform.name);

                if (hit.transform == player) return true;
            }
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}