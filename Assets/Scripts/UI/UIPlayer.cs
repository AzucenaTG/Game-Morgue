using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    public PlayerController scriptMovimiento;

    public Image rellenoStamina;
   
    void Update()
    {
        if (scriptMovimiento != null && rellenoStamina != null)
        {
           rellenoStamina.fillAmount = scriptMovimiento.currentStamina / scriptMovimiento.maxStamina;
        }
    }
}
