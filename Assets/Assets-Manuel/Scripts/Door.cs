using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f;
    public float speed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public bool requireKey = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closedRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) {

            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * speed);

        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,closedRotation,Time.deltaTime * speed);
        }
    }

    public void ToggleDoor()
    {
        InventarySystem inv = FindAnyObjectByType<InventarySystem>();

        if (requireKey)
        {
            if (inv == null || !inv.items.Contains("Key")) {


               
                return;
            }

            
            inv.items.Remove("Key");
            inv.display.RefreshUI();

            requireKey = false;
        }
        
        isOpen = !isOpen;

        Vector3 playerDir = (transform.position - Camera.main.transform.position).normalized;

        float dot = Vector3.Dot(transform.forward, playerDir);

        float angle = dot > 0 ? -openAngle : openAngle;

        openRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);
    }
}
