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
    if (flashLight == null)
        return;

    float value = flashLight.battery;

    if (value > 750)
        batteryImage.sprite = llena;
    else if (value > 500)
        batteryImage.sprite = media;
    else if (value > 250)
        batteryImage.sprite = casiBaja;
    else
        batteryImage.sprite = baja;
}
}
