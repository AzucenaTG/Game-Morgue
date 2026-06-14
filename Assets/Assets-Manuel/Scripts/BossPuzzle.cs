using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossPuzzle : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;
    public TMP_Text dialogText;

    public Button word1;
    public Button word2;
    public Button word3;

    [Header("Jugador")]
    public WASD playerMovement;
    public MouseLocker mouseLook;

    [Header("Victoria")]
    public Image whiteFade;
    public string victorySceneName = "VictoryScene";
    public float fadeDuration = 3f;

    private int currentStep = 0;

    void Start()
    {   
        dialogPanel.SetActive(false);

        Color color = whiteFade.color;
        color.a = 0f;
        whiteFade.color = color;

        word1.onClick.AddListener(() => CheckWord("LUZ"));
        word2.onClick.AddListener(() => CheckWord("SOMBRA"));
        word3.onClick.AddListener(() => CheckWord("SALIDA"));
    }

    public void StartPuzzle()
    {
        dialogPanel.SetActive(true);

        dialogText.text =
            "Soy el jefe final. Tu desafío para escapar es decir la frase en orden.";

        currentStep = 0;

        // Mostrar botones
        word1.gameObject.SetActive(true);
        word2.gameObject.SetActive(true);
        word3.gameObject.SetActive(true);

        // Mostrar cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Detener movimiento de cámara
        if (mouseLook != null)
        {
            mouseLook.enabled = false;
        }
    }

    void CheckWord(string word)
    {
        string[] correctOrder =
        {
            "LUZ",
            "SOMBRA",
            "SALIDA"
        };

        if (word == correctOrder[currentStep])
        {
            currentStep++;

            if (currentStep >= correctOrder.Length)
            {
                dialogText.text = "Perfecto, has escapado.";

                word1.gameObject.SetActive(false);
                word2.gameObject.SetActive(false);
                word3.gameObject.SetActive(false);

                StartCoroutine(VictorySequence());
            }
        }
        else
        {
            FailPuzzle();
        }
    }

    void FailPuzzle()
    {
        dialogPanel.SetActive(false);

        // Reactivar movimiento
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        // Reactivar cámara
        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ClosePanel()
    {
        dialogPanel.SetActive(false);

        if (mouseLook != null)
        {
            mouseLook.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator VictorySequence()
    {
        yield return new WaitForSeconds(2f);

        Color color = whiteFade.color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime * 0.5f;
            whiteFade.color = color;

            yield return null;
        }

        SceneManager.LoadScene(victorySceneName);
    }
}