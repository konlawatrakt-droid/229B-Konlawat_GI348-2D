using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ต้องมี { } ครอบเพื่อให้ทำงานเฉพาะ Player
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.SetCheckpoint(transform.position);
                Debug.Log("Checkpoint Saved!");
            }

            // สั่งยืนยันการตายศัตรูที่ผ่านมา
            if (EnemySaveManager.instance != null)
            {
                EnemySaveManager.instance.CommitDeaths();
            }
        }
    }
}