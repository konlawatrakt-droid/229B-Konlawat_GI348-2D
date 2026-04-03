using UnityEngine;

public class HealItem : MonoBehaviour
{
    public float healAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP hp = other.GetComponent<PlayerHP>();

            if (hp != null)
            {
                hp.Heal(healAmount);
                Debug.Log("ฮีล +" + healAmount);

                Destroy(gameObject);
            }
        }
    }
}