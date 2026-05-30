using UnityEngine;
using UnityEngine.UI;
public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public Camera playerCamera;
    public FlashLight flashLight;
    public InteractionUIDoorKey interactionUI;
    public GameObject keypadPanel;
    private bool panelOpen = false;
    public GameObject paperUI;
    private bool paperOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (panelOpen)
        {
            if (Input.GetMouseButton(0))
            {

                ClosePanel();
            }
            return;
        }

        if (paperOpen)
        {
            if (Input.GetMouseButton(0))
            {

                ClosePaper();
            }
            return;
        }


        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        InventarySystem inv = FindAnyObjectByType<InventarySystem>();

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Door door = hit.collider.GetComponentInParent<Door>();

            if (door != null)
            {

                if (door.requireKey)
                {
                    interactionUI.Show("Necesitas llave");
                }
                else
                {
                    interactionUI.Show("Presiona el click izquierdo para abrir");
                }

                if (Input.GetMouseButton(0))
                {

                    door.ToggleDoor();
                }

                return;

            }

            if (hit.collider.CompareTag("CodeBox"))
            {
                interactionUI.Show("Presiona el click izquierdo para usar");

                if (Input.GetMouseButton(0))
                {
                    if (!panelOpen)
                    {
                        OpenPanel();
                    }
                    else
                    {
                        ClosePanel();
                    }
                }
                return;

            }

            if (hit.collider.CompareTag("CodePaper"))
            {
                interactionUI.Show("Presiona el click izquierdo para usar");

                if (Input.GetMouseButton(0))
                {

                    OpenPaper();
                }

                return;
            }

           if (hit.collider.CompareTag("Battery"))
           {
                interactionUI.Show("Presiona el click izquierdo para usar");
                if (Input.GetMouseButton(0))
                {
                    if (inv.AddItem("Battery"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    
                }
                else if (hit.collider.CompareTag("Medic"))
                {
                interactionUI.Show("Presiona el click izquierdo para usar");
                if (Input.GetMouseButton(0))
                    {
                       
                        if (inv.AddItem("Medic"))
                        {
                            Destroy(hit.collider.gameObject);
                        }

                    }
                }
                else if (hit.collider.CompareTag("Sanity"))
                {
                interactionUI.Show("Presiona el click izquierdo para usar");
                if (Input.GetMouseButton(0))
                    {

                        if (inv.AddItem("Sanity"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    
                }
                else if (hit.collider.CompareTag("Key"))
                {
                interactionUI.Show("Presiona el click izquierdo para usar");
                if (Input.GetMouseButton(0)) {

                        if (inv.AddItem("Key"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    
                }

            return;
        }
        interactionUI.Hide();
    }

    void OpenPanel()
    {
        keypadPanel.SetActive(true);
        panelOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePanel()
    {
        keypadPanel.SetActive(false);
        panelOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OpenPaper()
    {
        paperUI.SetActive(true);
        paperOpen = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePaper()
    {
        paperUI.SetActive(false);
        paperOpen = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

