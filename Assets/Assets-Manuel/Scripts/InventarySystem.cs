using UnityEngine;
using System.Collections.Generic;

public class InventarySystem : MonoBehaviour
{
    public List<string> items = new List<string>();
    public FlashLight flashLight;
    public float healAmount = 25f;
    public float sanityAmount = 25f;
    public InventoryDsiplay display;
    public UIMessage uiMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(string item)
    {
       if(items.Count>= display.slotCount)
        {
            if(uiMessage != null)
            {
                uiMessage.ShowMessage();
            }
            return false;
        }
        
        items.Add(item);

        if (display != null)
        {

            display.RefreshUI();
        }

        return true;
       
    
    }

    public void UseItem(string item)
    {   
        if(item == "Key")
        {
            return;
        }
       
        if (item == "Battery")
        {
           flashLight.battery += 25f;
           flashLight.battery = Mathf.Clamp(flashLight.battery, 0, 100);
        }

        if (item == "Medic")
        {
            PlayerHealth health = GetComponent<PlayerHealth>();

            if (health != null)
            {

                health.currentHealth += healAmount;
                health.currentHealth = Mathf.Clamp(health.currentHealth, 0, health.maxHealth);
            }
        }

        if (item == "Sanity")
        {
            SanitySystem sanity = GetComponent<SanitySystem>();

            if (sanity != null)
            {

                sanity.currentSanity += sanityAmount;
                sanity.currentSanity = Mathf.Clamp(sanity.currentSanity, 0, sanity.maxSanity);
            }
        }

       items.Remove(item);

        if(display != null) 
        { 
            display.RefreshUI(); 
        }
    }
}
