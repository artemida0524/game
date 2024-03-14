using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Image image;
    private string currentName;
    private Image currentImage;

    [SerializeField] private ButtonSelect buttonSelect;
    [SerializeField] private Sprite noneImage;

    [SerializeField] string[] nameObjectResource;
    [SerializeField] int[] countObjectResource;

    [SerializeField] Image imageSelect;
    [SerializeField] TextMeshProUGUI nameSelect;

    [SerializeField] private SlotCraftSelectList selectList;
    [SerializeField] private TakeObject takeObject;
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private ScriptableItemList scriptableItemList;

    private void Start()
    {
        currentImage = image;
        currentName = name.text;
    }

    public void GetDataOfCraftItem()
    {
        imageSelect.sprite = image.sprite;
        nameSelect.text = name.text;
    }

    public void ViewData()
    {
        buttonSelect.currentName = currentName;
        buttonSelect.currentImage = currentImage;
        buttonSelect.countObjectResource = countObjectResource;
        buttonSelect.nameObjectResource = nameObjectResource;

        for (int i = 0; i < nameObjectResource.Length; i++)
        {
            for (int j = 0; j < scriptableItemList.scriptableItems.Length; j++)
            {
                if (nameObjectResource[i] == scriptableItemList.scriptableItems[j].id)
                {
                    selectList.slots[i].image.sprite = scriptableItemList.scriptableItems[j].sprite;

                    try
                    {
                        selectList.slots[i].count.text = $"{inventoryData.inventory[nameObjectResource[i]].count}/{countObjectResource[i]}";
                        
                    }
                    catch (System.Exception)
                    {
                        selectList.slots[i].count.text = $"{0}/{countObjectResource[i]}";
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        foreach (var item in selectList.slots)
        {
            item.count.text = "";
            item.image.sprite = noneImage;
        }

        imageSelect.sprite = noneImage;
        nameSelect.text = "";
        Debug.Log("disable");
    }


}