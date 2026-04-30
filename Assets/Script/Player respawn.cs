using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private CharacterController controller;
    private PlayerMovement movement;

    // ✅ เพิ่มตรงนี้
    private PlayerHP playerHP;               // สคริปต์ HP ของผู้เล่น
    private PlayerShooting playerShooting;   // สคริปต์ยิงของผู้เล่น

    void Start()
    {
        checkpointPosition = transform.position;
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();

        // ✅ ดึง component มาเก็บไว้
        playerHP = GetComponent<PlayerHP>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void Respawn()
    {
        Debug.Log("Respawning...");

        // 1. ย้ายตำแหน่งกลับ Checkpoint
        if (controller != null) controller.enabled = false;
        transform.position = checkpointPosition;
        if (movement != null) movement.ResetVelocity();
        if (controller != null) controller.enabled = true;

        // 2. ✅ เติมเลือดเต็ม
        if (playerHP != null)
        {
            playerHP.ResetHP();
            Debug.Log("HP Reset!");
        }

        // 3. ✅ เติมกระสุนเต็ม
        if (playerShooting != null)
        {
            playerShooting.AddAmmo(playerShooting.maxAmmo);
            Debug.Log("Ammo Refilled!");
        }

        // 4. Reset ศัตรู
        if (EnemySaveManager.instance != null)
        {
            EnemySaveManager.instance.ResetTempDeaths();
        }
    }
}