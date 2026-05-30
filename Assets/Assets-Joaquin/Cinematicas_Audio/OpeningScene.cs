using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private float time = 43f;

    [SerializeField] private string sceneName = "Main";


    void Start() 
    {
        StartCoroutine(TransitionToNextScene());
    }

    private IEnumerator TransitionToNextScene() 
    {
        yield return new WaitForSeconds(time); //Espera los segundos del time

        SceneManager.LoadScene(sceneName); //Cuando termina el tiempo, carga la escena main de la Morgue
    }
}
