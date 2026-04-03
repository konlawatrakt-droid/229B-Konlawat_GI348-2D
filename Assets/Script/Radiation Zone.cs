using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    public float damagePerSecond = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHP hp = other.GetComponent<PlayerHP>();
            if (hp != null)
            {
                hp.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}