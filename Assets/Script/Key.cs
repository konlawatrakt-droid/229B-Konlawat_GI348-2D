using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inv = other.GetComponent<PlayerInventory>();

            if (inv != null)
            {
                inv.keyCount += amount;
                Destroy(gameObject);
            }
        }
    }
}