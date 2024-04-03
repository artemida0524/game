using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;



public class InventoryBag : MonoBehaviour // MUST CHANGE
{
    [SerializeField] Image image;
    [SerializeField] public GameObject targetBag;
    [NonSerialized] public GameObject currentBag;

    [SerializeField] private Sprite imageBagDefault;

    public const string NAME_BAG = "BagCharacter";
    public InventoryItem inventoryItem;

    private void Awake()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }

    private void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }

    private void Update()
    {

        PlayerPrefs.SetString(NAME_BAG, inventoryItem.id);


        if (inventoryItem.id != string.Empty)
        {
            foreach (var item in inventoryItem.inventoryView.scriptableItemList.scriptableItems)
            {
                if (inventoryItem.id == item.id)
                {
                    image.sprite = item.sprite;
                }
            }
            
        }
        else
        {
            image.sprite = imageBagDefault;
        }
    }


    private void OnEnable()
    {
        if (inventoryItem.id != string.Empty)
        {
            Debug.Log(inventoryItem.id);
        }
    }
}
