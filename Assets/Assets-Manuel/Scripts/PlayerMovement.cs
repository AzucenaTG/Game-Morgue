using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    float yVelocity;
    public float gravity = -9.8f;
    public AudioSource footstepAudio; /*colocado por azu*/

    float xRotation = 0f;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool moving = Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f; /*colocado por azu*/

        if (moving && controller.isGrounded) /*colocado por azu*/
       {
          if (!footstepAudio.isPlaying)
          footstepAudio.Play();
        }
        else
        {
          footstepAudio.Stop();
        }

        Vector3 move = transform.right * x + transform.forward * z;

        // Gravedad
        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f; // lo mantiene pegado al piso
        }

        yVelocity += gravity * Time.deltaTime;

        move.y = yVelocity;

        controller.Move(move * speed * Time.deltaTime);

        // Mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
