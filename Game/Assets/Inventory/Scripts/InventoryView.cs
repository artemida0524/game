using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] public InventoryData inventoryData;
    [SerializeField] GameObject panel;
    [SerializeField] private InventoryItem[] inventoryItems;
    [SerializeField] public ScriptableItem[] scriptableItems;
    [SerializeField] public GameObject mainCharacter;
    private Color color1 = new Color(255, 255, 255, 255);
    private Color color2 = new Color(255, 255, 255, 0);




    private void OnEnable()
    {   
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //mainCharacter.GetComponent<Player1>().enabled = false;
        inventoryItems = GetComponentsInChildren<InventoryItem>();
        int iter = 0;
        foreach (var item in inventoryData.inventory)
        {
            if (item.Key != "")
            {
                inventoryItems[iter].image.color = color1;

                inventoryItems[iter].textMeshProUGUI.text = item.Value.count.ToString();

                for (int i = 0; i < scriptableItems.Length; i++)
                {
                    if(item.Key == scriptableItems[i].id)
                    {
                        inventoryItems[iter].image.sprite = scriptableItems[i].sprite;
                    }
                }
                iter++;
            }
        }
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //mainCharacter.GetComponent<Player1>().enabled = true;
        foreach (var item in inventoryItems)
        {
            item.image.color = color2;
            item.textMeshProUGUI.text = "";
        }
    }
}
