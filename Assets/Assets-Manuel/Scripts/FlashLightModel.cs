using UnityEngine;

public class FlashLightModel : MonoBehaviour
{
    public float swayAmount = 0.02f;
    public float smoothSpeed = 5f;

    Vector3 initialPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        Vector3 targetPosition = initialPosition + new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition,targetPosition, Time.deltaTime * smoothSpeed);
    }
}
