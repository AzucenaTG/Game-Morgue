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
    public Image paperUIImage;
    private bool paperOpen = false;

    void Update()
    {
        if (panelOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClosePanel();
            }
            return;
        }

        if (paperOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClosePaper();
            }
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

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
                    interactionUI.Show("Presiona click izquierdo para abrir");
                }

                if (Input.GetMouseButtonDown(0))
                {
                    door.ToggleDoor();
                }

                return;
            }

            if (hit.collider.CompareTag("CodeBox"))
            {
                interactionUI.Show("Presiona click izquierdo para usar");

                if (Input.GetMouseButtonDown(0))
                {
                    if (!panelOpen)
                    {
                        OpenPanel();
                    }
                }

                return;
            }

            if (hit.collider.CompareTag("CodePaper"))
            {
                interactionUI.Show("Presiona click izquierdo para leer");

                if (Input.GetMouseButtonDown(0))
                {
                    Paper paper = hit.collider.GetComponent<Paper>();

                    if (paper != null)
                    {
                        OpenPaper(paper.paperImage);
                    }
                }

                return;
            }

            if (hit.collider.CompareTag("Battery"))
            {
                interactionUI.Show("Presiona click izquierdo para recoger");

                if (Input.GetMouseButtonDown(0))
                {
                    if (inv.AddItem("Battery"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                return;
            }

            if (hit.collider.CompareTag("Medic"))
            {
                interactionUI.Show("Presiona click izquierdo para recoger");

                if (Input.GetMouseButtonDown(0))
                {
                    if (inv.AddItem("Medic"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                return;
            }

            if (hit.collider.CompareTag("Sanity"))
            {
                interactionUI.Show("Presiona click izquierdo para recoger");

                if (Input.GetMouseButtonDown(0))
                {
                    if (inv.AddItem("Sanity"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                return;
            }

            if (hit.collider.CompareTag("Key"))
            {
                interactionUI.Show("Presiona click izquierdo para recoger");

                if (Input.GetMouseButtonDown(0))
                {
                    if (inv.AddItem("Key"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                return;
            }
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

    void OpenPaper(Sprite image)
    {
        paperUIImage.sprite = image;

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