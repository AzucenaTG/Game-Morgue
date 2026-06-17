using UnityEngine; 
using UnityEngine.SceneManagement; 


public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Escena_Intro");
    }

       public void MenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}