using UnityEngine;
using UnityEngine.UI;

public class InventoryUi : MonoBehaviour
{
    public GameObject InventoryUI;
    private bool isOpen = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            InventoryUI.SetActive(isOpen);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;

        }  
    }
}
