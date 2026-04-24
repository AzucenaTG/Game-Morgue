using TMPro;
using UnityEngine;

public class keyPadUI : MonoBehaviour
{

    public TextMeshProUGUI displayText;
    public BoxPassword box;

    private string currentInput = "";

    public GameObject panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressNumber(string number)
    {
        currentInput += number;
        UpdateDisplay();
    }

    public void PressClear()
    {
        currentInput = "";
        UpdateDisplay();
    }

    public void PressOk()
    {
        box.AddDigit("");
        CheckCode();
    }

    void CheckCode()
    {
        if(currentInput == box.correctCode)
        {
            box.SendMessage("OpenBox");
        }
        else
        {
            Debug.Log("Worng Code");
        }

        currentInput = "";
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

}
