using UnityEngine;
using TMPro;
using System.Collections;

public class UIMessage : MonoBehaviour
{

    public GameObject messageObject;
    public float duration = 2f;
    private Coroutine currentRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMessage()
    {
        if (currentRoutine != null)
        {

            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        messageObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        messageObject.SetActive(false);
    }
}
