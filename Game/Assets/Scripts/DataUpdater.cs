using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;


public class DataUpdater : MonoBehaviour
{
    private TextMeshProUGUI timeFuel;
    [SerializeField] private TextMeshProUGUI timeResult;
    [SerializeField] private TextMeshProUGUI textMeshProUGUICountResource;
    [SerializeField] private TextMeshProUGUI textMeshProUGUICountFuel;
    [SerializeField] private TextMeshProUGUI textMeshProUGUICountResult;

    [SerializeField] private GameObject gameObjectResult;
    [SerializeField] private GameObject gameObjectFuel;
    [SerializeField] private GameObject gameObjectResource;

    [SerializeField] private Image imageResource;
    [SerializeField] private Image imageFuel;
    [SerializeField] private Image imageResult;

    [SerializeField] private Sprite backgroundImage;
    [SerializeField] private InventoryView inventoryView;

    private void Start()
    {
        timeFuel = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {


        timeResult.text = TimeConvert(inventoryView.furnace.timeResult);
        timeFuel.text = TimeConvert(inventoryView.furnace.timeFuel);

        //Resource
        if (inventoryView.furnace.countResource != 0)
        {
            textMeshProUGUICountResource.text = inventoryView.furnace.countResource.ToString();
            gameObjectResource.GetComponent<InventoryItem>().id = inventoryView.furnace.idResource;
        }
        else
        {
            textMeshProUGUICountResource.text = "";
            imageResource.sprite = backgroundImage;
        }


        //Fuel
        if (inventoryView.furnace.countFuel != 0)
        {
            textMeshProUGUICountFuel.text = inventoryView.furnace.countFuel.ToString();
            gameObjectFuel.GetComponent<InventoryItem>().id = inventoryView.furnace.idFuel;
        }
        else
        {
            textMeshProUGUICountFuel.text = "";
            imageFuel.sprite = backgroundImage;
        }

        //Result
        if (inventoryView.furnace.idResult != "")
        {
            textMeshProUGUICountResult.text = inventoryView.furnace.countResult.ToString();
            gameObjectResult.GetComponent<InventoryItem>().id = inventoryView.furnace.idResult;
            foreach (var item in inventoryView.scriptableItemList.scriptableItems)
            {
                if (item.id == inventoryView.furnace.idResult)
                {
                    imageResult.sprite = item.sprite;
                }
            }
        }
        else
        {
            textMeshProUGUICountResult.text = "";
            imageResult.sprite = backgroundImage;
        }

    }

    public string TimeConvert(float time)
    {
        return time != 0 ? $"{(int)time / 60}:{(int)time % 60}" : string.Empty;
    }

    public static string TimeConvert(int time)
    {
        return time != 0 ? $"{time / 60}:{time % 60}" : string.Empty;
    }

}
