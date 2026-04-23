using UnityEngine;

public class BoxPassword : MonoBehaviour
{

    public string correctCode = "12345";
    private string currentInput = "";

    public Transform lidHinge;
    public float openSpeed = 2f;
    private bool isOpen = false;

    public Vector3 openRotation = new Vector3(0f,90f,0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen && lidHinge != null) {
        
            lidHinge.localRotation = Quaternion.Lerp(
                
                lidHinge.localRotation,
                Quaternion.Euler(openRotation),
                Time.deltaTime * openSpeed 
            );
        }
    }

    public void AddDigit(string digit)
    {
        currentInput += digit;
        Debug.Log(currentInput);
    }

    public void CheckCode()
    {
        if(currentInput == correctCode)
        {
            Debug.Log("box opened");
            OpenBox();
        }
        else
        {
            Debug.Log("Wrong code");
        }

        ResetInput();
    }

    public void OpenBox()
    {
        isOpen = true;
    }

    public void ResetInput()
    {
        currentInput = "";
    }
}
