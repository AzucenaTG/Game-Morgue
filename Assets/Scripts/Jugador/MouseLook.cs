using UnityEngine;

public class MouseLocker : MonoBehaviour
{
    public float sensibility = 100f; //Velocidad de la camara
    public Transform bodyPlayer;

    float rotacionX = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloquea el mouse en el centro de la camara
    }

    void Update()
    {
        // Obtener movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        //Rotaciones
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        bodyPlayer.Rotate(Vector3.up * mouseX);
    }
}
