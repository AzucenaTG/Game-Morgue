using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{

    public TextMeshProUGUI text;
    private string itemName;
    private InventarySystem inventory;
    public Image icon;
    public Sprite batteryIcon;
    public Sprite medicIcon;
    public Sprite sanityIcon;
    public Sprite keyIcon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string name,InventarySystem inv,int amount)
    {
        itemName = name;
        inventory = inv;

        if(name == "Battery")
        {
            icon.sprite = batteryIcon;
        }
        else if (name == "Medic")
        {
            icon.sprite = medicIcon;
        }
        else if (name == "Sanity")
        {
            icon.sprite = sanityIcon;
        }
        else if (name == "Key")
        {
            icon.sprite = keyIcon;
        }
    }

    public void OnClick()
    {
        if(itemName == "Key")
        {
            return;
        }

        if (inventory != null)
        {
            inventory.UseItem(itemName);
         
        }
       
    }

    public void Clear()
    {
        text.text = "";
        icon.sprite = null;
        Button btn = GetComponent<Button>();
        btn.interactable = false;
       

    }
}
