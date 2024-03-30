using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceItem : MonoBehaviour
{
    private InventoryItem inventoryItem;
    [SerializeField] private TakeObject takeObject;
    [SerializeField] private InventoryView inventoryView;
    [SerializeField] private Sprite backgroundImage;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private GameObject CanvasFurnace;

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

    private void OnEnable()
    {
        takeObject.isTake = false;
        inventoryItem = GetComponent<InventoryItem>();

        foreach (var item in inventoryView.scriptableItemList.scriptableItems)
        {
            if (item.id == inventoryView.furnace.idResource)
            {
                resourceImage.sprite = item.sprite;
                resourceCount.text = $"{inventoryView.furnace.countResource}";
            }

            if (item.id == inventoryView.furnace.idFuel)
            {
                fuelImage.sprite = item.sprite;
                fuelCount.text = $"{inventoryView.furnace.countFuel}";
            }
        }

        inventoryView.furnace.textMeshProUGUICountResource = resourceCount;
        inventoryView.furnace.textMeshProUGUICountFuel = fuelCount;
        inventoryView.furnace.scriptableItemList = inventoryView.scriptableItemList;
    }


    public void ThrowInInventoryFromFurnace()
    {
        Debug.Log(inventoryItem.id);
    }

    public void ThrowInFurnace()
    {
        inventoryItem = GetComponent<InventoryItem>();
        foreach (var item in inventoryItem.inventoryView.scriptableItemList.scriptableItems)
        {
            if (item.id == inventoryItem.id && item.furnaceMode == FurnaceMode.Resource)
            {
                if (inventoryView.furnace.idResource == item.id)
                {

                    inventoryView.furnace.countResource += int.Parse(inventoryItem.textMeshProUGUI.text);
                    inventoryView.inventoryData.RemoveData(inventoryItem.id, int.Parse(inventoryItem.textMeshProUGUI.text));

                    StartCoroutine(SetFurnace());
                }
                else
                {
                    resourceImage.sprite = inventoryItem.image.sprite;
                    resourceCount.text = inventoryItem.textMeshProUGUI.text;

                    inventoryView.furnace.idResource = item.id;
                    inventoryView.furnace.countResource = int.Parse(inventoryItem.textMeshProUGUI.text);

                    inventoryView.inventoryData.RemoveData(inventoryItem.id, int.Parse(inventoryItem.textMeshProUGUI.text));
                    StartCoroutine(SetFurnace());
                }
            }
            if (item.id == inventoryItem.id && item.furnaceMode == FurnaceMode.Fuel)
            {

                if (inventoryView.furnace.idFuel == item.id)
                {
                    
                    inventoryView.furnace.countFuel += int.Parse(inventoryItem.textMeshProUGUI.text);
                    inventoryView.inventoryData.RemoveData(inventoryItem.id, int.Parse(inventoryItem.textMeshProUGUI.text));

                    StartCoroutine(SetFurnace());
                }
                else
                {
                    if (inventoryView.furnace.idFuel != "")
                    {
                        Debug.Log("if");
                        inventoryView.inventoryData.AddData(inventoryView.furnace.idFuel, new ObjectData(inventoryView.furnace.countFuel, inventoryView.furnace.idFuel));

                        fuelImage.sprite = inventoryItem.image.sprite;
                        fuelCount.text = inventoryItem.textMeshProUGUI.text;

                        inventoryView.furnace.idFuel = item.id;
                        inventoryView.furnace.countFuel = int.Parse(inventoryItem.textMeshProUGUI.text);

                        inventoryView.inventoryData.RemoveData(inventoryItem.id, int.Parse(inventoryItem.textMeshProUGUI.text));
                        StartCoroutine(SetFurnace());


                    }
                    else
                    {
                        Debug.Log("else");
                        fuelImage.sprite = inventoryItem.image.sprite;
                        fuelCount.text = inventoryItem.textMeshProUGUI.text;

                        inventoryView.furnace.idFuel = item.id;
                        inventoryView.furnace.countFuel = int.Parse(inventoryItem.textMeshProUGUI.text);

                        inventoryView.inventoryData.RemoveData(inventoryItem.id, int.Parse(inventoryItem.textMeshProUGUI.text));
                        StartCoroutine(SetFurnace());
                    }

                }
            }
        }
    }

    private void OnDisable()
    {
        resourceImage.sprite = backgroundImage;
        resourceCount.text = "";

        fuelImage.sprite = backgroundImage;
        fuelCount.text = "";

        inventoryItem.image.sprite = backgroundImage;
        inventoryItem.textMeshProUGUI.text = "";

        takeObject.isTake = true;
        //inventoryView.furnace.time.text = "";
    }


    private IEnumerator SetFurnace()
    {
        yield return new WaitForSeconds(0.01f);
        CanvasFurnace.SetActive(false);
        CanvasFurnace.SetActive(true);

    }
}
