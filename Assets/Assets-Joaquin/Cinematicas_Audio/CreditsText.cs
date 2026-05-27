using UnityEngine;
using TMPro;
using System.Collections;



public class CreditsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float tiempoPorPantalla = 5.5f;
    [SerializeField] private float duracionFade = 1.0f;

    [SerializeField] private string[] creditsList = new string[]
    {
        "MORTUARY ENGINES\nUn juego hecho por:",
        "Joaquin Acuña",
        "Manuel Arzuaga",
        "Juan José Bigeón",
        "Kevin Cossi",
        "Sebastian Ravecca",
        "María Azucena Torres Gorlero"
    };

    void Start()
    {
        StartCoroutine(PlayCreditsSequence());  
    }

    private IEnumerator PlayCreditsSequence()
    {
        //Se fuerza el Alfa 0
        textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0);

        //Se recorre todo el array
        for (int i = 0; i < creditsList.Length; i++)
        {
            textComponent.text = creditsList[i];

            //Fade In
            yield return StartCoroutine(FadeText(0f, 1f));

            //Esperar
            yield return new WaitForSeconds(tiempoPorPantalla - (duracionFade * 2f));

            //Fade out
            yield return StartCoroutine(FadeText(1f, 0f));

            //Espera a que aparezca el siguiente texto
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha) 
    {
        float elapsedTime = 0f;
        Color currentColor = textComponent.color;

        while (elapsedTime < duracionFade)
        {
            elapsedTime += Time.deltaTime;
            //Interpolación para suavizar la transparencia
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duracionFade);
            
            textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            yield return null;
        }
    }
}
