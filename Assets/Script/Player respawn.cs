using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private CharacterController controller;
    private PlayerMovement movement; // สมมติว่าคุณมีสคริปต์นี้ควบคุมการเคลื่อนที่

    void Start()
    {
        checkpointPosition = transform.position;
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
    }

    public void Respawn()
    {
        Debug.Log("Respawning...");

        // ปิด Controller ก่อนย้ายตำแหน่ง (ป้องกันการติดบั๊กฟิสิกส์)
        if (controller != null) controller.enabled = false;

        transform.position = checkpointPosition;

        if (movement != null) movement.ResetVelocity();
        if (controller != null) controller.enabled = true;

        // 1. ล้างรายการศัตรูที่ตายระหว่างทาง
        // 2. ปลุกศัตรูที่ "ยังไม่ตายถาวร" กลับคืนมา
        if (EnemySaveManager.instance != null)
        {
            EnemySaveManager.instance.ResetTempDeaths();
        }
    }
}