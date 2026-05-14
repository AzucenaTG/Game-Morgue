using UnityEngine; 
using UnityEngine.SceneManagement; 


public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main");
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