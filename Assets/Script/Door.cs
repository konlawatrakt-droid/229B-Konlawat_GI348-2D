using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredKeys = 3;

    public void TryOpen(GameObject player)
    {
        PlayerInventory inv = player.GetComponent<PlayerInventory>();

        if (inv != null && inv.keyCount >= requiredKeys)
        {
            Debug.Log("เปิดประตู!");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("กุญแจไม่พอ!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TryOpen(other.gameObject);
        }
    }
}