using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredKeys = 3;

    public void TryOpen(GameObject player)
    {
        PlayerInventory inv = player.GetComponent<PlayerInventory>();

        if (inv != null && inv.keyCount >= requiredKeys)
        {
            Debug.Log("à»Ô´»ÃÐµÙ!");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("¡Ø­á¨äÁè¾Í!");
        }
    }
}