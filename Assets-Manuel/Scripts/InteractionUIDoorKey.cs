using UnityEngine;
using TMPro;

public class InteractionUIDoorKey : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string message)
    {
        text.text = message;
        text.gameObject.SetActive(true);
    }

    public void Hide()
    {
        text.gameObject.SetActive(false);
    }
}
