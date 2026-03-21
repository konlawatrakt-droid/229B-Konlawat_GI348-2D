using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f; // ความสูงการกระโดด
    public Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ✅ เช็คว่าติดพื้นไหม
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // กันไม่ให้ลอย
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 move = camForward * vertical + camRight * horizontal;

        controller.Move(move * speed * Time.deltaTime);

        // ✅ กระโดด (กด Space)
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}