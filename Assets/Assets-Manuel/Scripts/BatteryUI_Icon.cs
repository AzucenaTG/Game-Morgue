using UnityEngine;
using UnityEngine.UI;

public class BatteryUI_Icon : MonoBehaviour
{
    public Image batteryImage;
    public FlashLight flashLight;

    public Sprite llena;
    public Sprite media;
    public Sprite casiBaja;
    public Sprite baja;

    void Update()
    {
        float value = flashLight.battery;

        if (value > 75)
            batteryImage.sprite = llena;
        else if (value > 50)
            batteryImage.sprite = media;
        else if (value > 25)
            batteryImage.sprite = casiBaja;
        else
            batteryImage.sprite = baja;
    }
}
/*script hecho por Azucena de la bateria interfaz*/