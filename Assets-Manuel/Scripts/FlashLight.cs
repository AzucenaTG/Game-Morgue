using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public Light flashLight;

    public float battery = 100f;
    public float drainSpeed = 5f;
    public float lowBattery = 20f;

    public float minIntensity = 1f;
    public float maxIntensity = 3f;

    bool isOn = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn;
            flashLight.enabled = isOn;
        }

        if (!isOn)
        {
            return;
        }

        battery -= drainSpeed * Time.deltaTime;
        battery = Mathf.Clamp(battery, 0, 100);

        if(battery <= 0)
        {
            flashLight.enabled = false;
            return;
        }

        if(battery <= lowBattery)
        {
            float flickerSpeed = Mathf.Lerp(0.1f, 0.01f, battery / lowBattery);

            if (Random.value < flickerSpeed) {

                flashLight.enabled = !flashLight.enabled;
            }

            flashLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
        else
        {
            flashLight.intensity = maxIntensity;
        }
    }
}
