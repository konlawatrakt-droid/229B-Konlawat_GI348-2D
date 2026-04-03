using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door; // ลาก Door มาใส่

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.TryOpen(other.gameObject);
        }
    }
}