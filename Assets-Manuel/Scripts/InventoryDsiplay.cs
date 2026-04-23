using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;


public class InventoryDsiplay : MonoBehaviour
{

    public InventarySystem inventory;
    //public TextMeshProUGUI text;
    public GameObject slotButtonPrefab;
    public Transform container;


    public int slotCount = 8;

    private List<GameObject> slots = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        CreateSlots();
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSlots()
    {
        
        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotButtonPrefab, container);
            slots.Add(obj);
        }
    }

    public void RefreshUI()
    {
        int index = 0;

        foreach(string itemName in inventory.items)
        {
            if (index >= slots.Count)
            {
                break;
            }
            GameObject obj = slots[index];
            InventoryItemUI itemUI = obj.GetComponent<InventoryItemUI>();

            itemUI.Setup(itemName,inventory,1);
            
            if (itemUI == null) {

                continue;
            }

            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => inventory.UseItem(itemName));
            btn.interactable = true;
            
            index++;
        }

        for (int i = index; i < slots.Count; i++) {

            GameObject obj = slots[i];
            InventoryItemUI itemUI = obj.GetComponent<InventoryItemUI>();
            itemUI.Clear();
        }
    }
}
