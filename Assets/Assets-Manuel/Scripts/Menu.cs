using UnityEngine; 
using UnityEngine.SceneManagement; 


public class Menu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }
}