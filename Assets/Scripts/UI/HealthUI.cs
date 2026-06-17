using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    public Slider healthSlider;
    public PlayerHealth health;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health.currentHealth / health.maxHealth;    
    }
}
