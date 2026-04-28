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
        if (!playerInRange) return false; // เช็ค trigger ก่อน

        Vector3 dir = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dir);

        if (angle < viewAngle / 2f)
        {
            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewDistance, ~obstacleMask))
            {
                return hit.transform == player;
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