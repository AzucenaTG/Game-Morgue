using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{

    public Slider slider;
    public FlashLight flashLight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = flashLight.battery / 100f;
    }
}
