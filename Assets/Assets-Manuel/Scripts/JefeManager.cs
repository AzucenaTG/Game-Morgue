using UnityEngine;
using TMPro;
using System.Collections;

public class BossChallenge : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;
    public TMP_Text dialogText;

    public GameObject challengePanel;

    [Header("Cámara")]
    public Camera mainCamera;
    public Transform bossTarget;

    private Vector3 originalCameraPos;
    private float originalSize;

    private int currentIndex = 0;

    private string[] correctOrder =
    {
        "palabra1",
        "palabra2",
        "palabra3"
    };

    public void StartChallenge()
    {
        StartCoroutine(ChallengeSequence());
    }

    IEnumerator ChallengeSequence()
    {
        currentIndex = 0;

        originalCameraPos = mainCamera.transform.position;
        originalSize = mainCamera.orthographicSize;

        // Enfocar al jefe
        mainCamera.transform.position = new Vector3(
            bossTarget.position.x,
            bossTarget.position.y,
            originalCameraPos.z
        );

        mainCamera.orthographicSize = 3;

        dialogPanel.SetActive(true);

        dialogText.text = "Este es el jefe final. Tienes que hacer un desafío.";
        yield return new WaitForSeconds(3);

        dialogText.text = "El desafío es poner en orden las siguientes palabras.";
        yield return new WaitForSeconds(3);

        dialogPanel.SetActive(false);

        challengePanel.SetActive(true);
    }

    public void WordPressed(string word)
    {
        if (word == correctOrder[currentIndex])
        {
            currentIndex++;

            if (currentIndex >= correctOrder.Length)
            {
                StartCoroutine(CorrectChallenge());
            }
        }
        else
        {
            StartCoroutine(FailedChallenge());
        }
    }

    IEnumerator CorrectChallenge()
    {
        challengePanel.SetActive(false);

        dialogPanel.SetActive(true);
        dialogText.text = "Correcto. Superaste el desafío.";

        yield return new WaitForSeconds(3);

        dialogPanel.SetActive(false);

        EndSequence();
    }

    IEnumerator FailedChallenge()
    {
        challengePanel.SetActive(false);

        dialogPanel.SetActive(true);
        dialogText.text = "Orden incorrecto.";

        yield return new WaitForSeconds(3);

        dialogPanel.SetActive(false);

        EndSequence();
    }

    void EndSequence()
    {
        mainCamera.transform.position = originalCameraPos;
        mainCamera.orthographicSize = originalSize;

        currentIndex = 0;
    }
}