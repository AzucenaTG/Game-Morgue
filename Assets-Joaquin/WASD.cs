using UnityEngine;

public class WASD : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterController controller;
    public Transform cameraTransform;

    [Header("Velocidades")]
    public float walkSpeed = 4f; //Velocidad de movimiento caminando
    public float runSpeed = 8f; //Velocidad de movimiento corriendo
    public float crouchSpeed = 2f; //Velocidad de movimiento agachado

    [Header("Estamina")]
    public float maxStamina = 5f; //Segundos maximos corriendo
    public float staminaRegen = 1f; //Regenaracion

    public float currentStamina;
    private bool isExhausted = false; //Esta variable es para cuando el jugador agote la estamina, imposibilitando que corra

    [Header("Ajustes de camara")]
    private float defaultYPos;
    public float crouchYPos = 0.8f; //Altura aproximadamente a la mitad
    public float smoothSpeed = 10f; //Velocidad de la transicion entre parado y agachado
    private float currentBaseCamY; //Evita conflictos con el Lerp

    [Header("Ajustes del cuerpo (Collider)")]
    private float defaultHeight;
    private float defaultCenterY;
    public float crouchHeight = 0.5f;  //La mitad de la altura normal del Character Controller
    public float crouchCenterY = 0.5f; //Para que los pies se queden en el suelo

    [Header("Balanceo al correr (Bobbing)")]
    public float runBobFrequency = 11f; //Que tan rapido son los pasos
    public float runBobAmount = 0.08f; //Que tanto sube y baja la camara
    private float timer = 0f; //Cronometro para la "onda" matematica



    private void Start()
    {
        //Valores originales
        defaultYPos = cameraTransform.localPosition.y;
        currentBaseCamY = defaultYPos;
        defaultHeight = controller.height;
        defaultCenterY = controller.center.y;
        currentStamina = maxStamina;
    }

    void Update()
    {
        //Obtener los ejes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = (x != 0 || z != 0);
        bool isRunning = false;

        //Variables temporales de estado
        float currentSpeed = walkSpeed;
        float targetCamY = defaultYPos;
        float targetHeight = defaultHeight;
        float targetCenterY = defaultCenterY;


        //Comprobar si se presiona control izquierdo para agacharse
        if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
            targetCamY = crouchYPos;
            targetHeight = crouchHeight;
            targetCenterY = crouchCenterY;
        }

        //Comprobar si se presiona shift para correr
        else if (Input.GetKey(KeyCode.LeftShift) && !isExhausted)
        {
            currentSpeed = runSpeed;
            isRunning = true;
        }

        //Manejo de la estamina
        if (isRunning && isMoving)
        {
            currentStamina -= Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isExhausted = true;
                currentSpeed = walkSpeed;
                isRunning = false;
            }

        }

        //Regeneracion
        else
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += Time.deltaTime * staminaRegen;

                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }

                if (currentStamina >= 2f)
                {
                    isExhausted = false;
                }
            }
        }


        //Aplicar movimiento horizontal
        Vector3 move = transform.right * x + transform.forward * z;

        //Aplicar gravedad constante
        move.y = -9.81f;

        //Mover personaje con el Character Controller
        controller.Move(move * currentSpeed * Time.deltaTime);

        //Suavizado (Lerp) del Cuerpo
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * smoothSpeed);
        controller.center = new Vector3(
            controller.center.x,
            Mathf.Lerp(controller.center.y, targetCenterY, Time.deltaTime * smoothSpeed),
            controller.center.z);


        //Calculo de la altura base suavizada (estar de pie vs agachado)
        currentBaseCamY = Mathf.Lerp(currentBaseCamY, targetCamY, Time.deltaTime * smoothSpeed);

        //Calculo del desvio del balanceo
        float bobbingOffset = 0f;

        if (isRunning && isMoving)
        {
            timer += Time.deltaTime * runBobFrequency;
            bobbingOffset = Mathf.Sin(timer) * runBobAmount;
        }
        else
        {
            timer = 0f; // Reiniciamos el ciclo de la "ola" si nos detenemos
        }

        //Se aplica la suma a la camara final
        cameraTransform.localPosition = new Vector3(
            cameraTransform.localPosition.x,
            currentBaseCamY + bobbingOffset, //Se suma la base + la ola
            cameraTransform.localPosition.z
        );

    }
}
