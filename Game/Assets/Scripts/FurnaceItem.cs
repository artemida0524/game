using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceItem : MonoBehaviour
{
    private InventoryItem inventoryItem;
    [SerializeField] private Furnace furnace;

    //[SerializeField] private Image image;
    //[SerializeField] private TextMeshProUGUI count;

    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceCount;
    //private GameObject resourceGameObject;

    [SerializeField] private Image fuelImage;
    [SerializeField] private TextMeshProUGUI fuelCount;
    //private GameObject fuelGameObject;


    private void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }
    public void ThrowInFurnace()
    {
        inventoryItem = GetComponent<InventoryItem>();
        foreach (var item in inventoryItem.inventoryView.scriptableItemList.scriptableItems)
        {
            if(item.id == inventoryItem.id && item.furnaceMode == FurnaceMode.Resource)
            {
                resourceImage.sprite = inventoryItem.image.sprite;
                resourceCount.text = inventoryItem.textMeshProUGUI.text;
            }
            if(item.id == inventoryItem.id && item.furnaceMode == FurnaceMode.Fuel)
            {
                fuelImage.sprite = inventoryItem.image.sprite;
                fuelCount.text = inventoryItem.textMeshProUGUI.text;
            }
        }
    }


}
