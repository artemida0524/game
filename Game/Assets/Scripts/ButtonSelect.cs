using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] ScriptableItemList scriptableItemList;
    [SerializeField] SlotCraftSelectList slotCraftSelectList;
    [SerializeField] InventoryView mainInventoryView;

    [NonSerialized] public Image currentImage;
    [NonSerialized] public string currentName = "";

    [NonSerialized] public string[] nameObjectResource;
    [NonSerialized] public int[] countObjectResource;

    private bool isSelect = false;


    //[SerializeField] private TakeObject takeObject;



    public void Select()
    {
        if (currentName != "")
        {
            for (int i = 0; i < countObjectResource.Length; i++)
            {
                try
                {
                    if (inventoryData.inventory[nameObjectResource[i]].count >= countObjectResource[i])
                    {
                        isSelect = true;
                    }
                    else
                    {
                        isSelect = false;
                        break;
                    }
                }
                catch (KeyNotFoundException)
                {
                    isSelect = false;
                    break;
                }

            }

            if (inventoryData.inventory.ContainsKey(currentName))
            {
                isSelect = true;
            }
            else
            {
                if (inventoryData.inventory.Count >= mainInventoryView.SizeInventory - 1)
                {
                    isSelect = false;
                    Debug.Log("false");
                }
            }

            if (isSelect)
            {
                for (int i = 0; i < countObjectResource.Length; i++)
                {
                    inventoryData.RemoveData(nameObjectResource[i], countObjectResource[i]);

                }

                if (!inventoryData.inventory.ContainsKey(currentName))
                {
                    inventoryData.AddData(currentName, new ObjectData(1, currentName));
                }
                else
                {
                    inventoryData.inventory[currentName].count++;
                }

                for (int i = 0; i < nameObjectResource.Length; i++)
                {
                    for (int j = 0; j < scriptableItemList.scriptableItems.Length; j++)
                    {
                        if (nameObjectResource[i] == scriptableItemList.scriptableItems[j].id)
                        {
                            slotCraftSelectList.slots[i].image.sprite = scriptableItemList.scriptableItems[j].sprite;

                            try
                            {
                                slotCraftSelectList.slots[i].count.text = $"{inventoryData.inventory[nameObjectResource[i]].count}/{countObjectResource[i]}";

                            }
                            catch (System.Exception)
                            {
                                slotCraftSelectList.slots[i].count.text = $"{0}/{countObjectResource[i]}";
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Don't buy");
            }
        }
        else
        {
            Debug.Log("Empty");
        }
    }

    private void OnDisable()
    {
        currentName = "";
        currentImage = null;

        nameObjectResource = null;
        countObjectResource = null;
    }

}
