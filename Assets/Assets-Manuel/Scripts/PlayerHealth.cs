using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
