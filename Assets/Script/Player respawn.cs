using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private CharacterController controller;
    private PlayerMovement movement;

    void Start()
    {
        checkpointPosition = transform.position;
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = newCheckpoint;
        Debug.Log("Checkpoint Saved!");
    }

    public void Respawn()
    {
        Debug.Log("Respawn ไปที่: " + checkpointPosition);

        controller.enabled = false;

        transform.position = checkpointPosition;

        movement.ResetVelocity();

        controller.enabled = true;
    }
}