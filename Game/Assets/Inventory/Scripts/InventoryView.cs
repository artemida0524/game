using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    public const int DEFAULT_SIZE_INVENTORY = 10;

    

    [SerializeField] public InventoryData inventoryData;
    public Dictionary<string, ObjectData> resource;
    public Furnace furnace;
    [SerializeField] public InventoryItem[] inventoryItems;
    [SerializeField] public TakeObject takeObject;
    [SerializeField] public InventoryBag bag;
    [SerializeField] public ScriptableItemList scriptableItemList;
    [SerializeField] public InventoryView sideInventoryView;
    [SerializeField] public GameObject mainInventory;
    private int sizeInventory;
    public int SizeInventory { get => sizeInventory; set { sizeInventory = value; } }
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private InteractorForObjects interactorForObjects;
    private Color color1 = new Color(255, 255, 255, 255);
    private Color color2 = new Color(255, 255, 255, 0);

    private bool isInitialization = true;

    private string currentBagCharacter = string.Empty;

    private void Start()
    {
        
    }

    private void Awake()
    {

        
        if (TryGetComponent<BoxWithResource>(out BoxWithResource component))
        {
            sizeInventory = takeObject.box.sizeBox;
            inventoryItems = GetComponentsInChildren<InventoryItem>();
            Debug.Log("awake" + " " + name);
            return;
        }
        try
        {
            inventoryItems = GetComponentsInChildren<InventoryItem>();

            //sizeInventory = inventoryItems.Length;

            SetBagCharacter();
        }
        catch (System.Exception)
        {

        }

    }

    private void SetBagCharacter()
    {
        if (bag.inventoryItem.id != string.Empty)
        {
            foreach (var item in scriptableItemList.scriptableItems)
            {
                if (bag.inventoryItem.id == item.id)
                {
                    sizeInventory = DEFAULT_SIZE_INVENTORY + item.countItemsBag;    
                }
            }
        }
        else
        {
            sizeInventory = DEFAULT_SIZE_INVENTORY;
        }
        //mainInventory.SetActive(false);
        currentBagCharacter = bag.inventoryItem.id;
    }

    private void OnEnable()
    {


        if (isInitialization)
        {

            Awake();
            OnDisable();
            isInitialization = false;
            Debug.Log(SizeInventory + " " + name);
        }

        
        if (TryGetComponent<BoxWithResource>(out BoxWithResource component))
        {
            sizeInventory = takeObject.box.sizeBox;
        }

        if (!TryGetComponent<BoxWithResource>(out BoxWithResource component2))
        {
            if (currentBagCharacter != bag.inventoryItem.id)
            {
                Debug.Log("Set" + " " + name);
                SetBagCharacter();
            }

        }

        //if (TryGetComponent<BoxWithResource>(out BoxWithResource component))
        //{

        //    inventoryItems = GetComponentsInChildren<InventoryItem>();
        //    sizeInventory = inventoryItems.Length;
        //}

        try
        {
            interactorForObjects.isInteractor = false;
        }
        catch { }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (inventoryData != null)
        {
            resource = inventoryData.inventory;
        }

        //if (!TryGetComponent<BoxWithResource>(out BoxWithResource component1))
        //{
        for (int i = 0; i < sizeInventory; i++)
        {
            inventoryItems[i].gameObject.SetActive(true);
        }

        //}


        int iter = 0;
        foreach (var item in resource)
        {

            if (item.Key != "")
            {


                inventoryItems[iter].image.color = color1;
                inventoryItems[iter].id = item.Value.id;
                inventoryItems[iter].textMeshProUGUI.text = item.Value.count.ToString();

                for (int i = 0; i < scriptableItemList.scriptableItems.Length; i++)
                {
                    if (item.Key == scriptableItemList.scriptableItems[i].id)
                    {
                        inventoryItems[iter].image.sprite = scriptableItemList.scriptableItems[i].sprite;
                    }
                }
                iter++;

            }
        }

        //foreach (var item in inventoryItems)
        //{
        //    Debug.Log(item.id + " " + item.textMeshProUGUI.text);
        //}


    }

    private void OnDisable()
    {
        try
        {
            interactorForObjects.isInteractor = true;
        }
        catch { }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //mainCharacter.GetComponent<Player1>().enabled = true;
        foreach (var item in inventoryItems)
        {

            if (item.gameObject.TryGetComponent<InventoryBag>(out InventoryBag component))
            {
                continue;
            }

            item.image.color = color2;
            item.textMeshProUGUI.text = "";
            item.id = "";

            //if (!TryGetComponent<BoxWithResource>(out BoxWithResource component1))
            //{
            item.gameObject.SetActive(false);
            //}

            try
            {
                foreach (var item1 in inventoryItems)
                {
                    item1.gameObject.GetComponent<PointerButton>().lead = false;
                    item1.gameObject.GetComponent<PointerButton>().time = 0f;
                }

            }
            catch (System.Exception)
            {
            }
        }
    }
}
