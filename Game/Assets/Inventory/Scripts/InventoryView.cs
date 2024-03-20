using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] public InventoryData inventoryData;
    public Dictionary<string, ObjectData> resource;
    [SerializeField] public InventoryItem[] inventoryItems;
    [SerializeField] public ScriptableItemList scriptableItemList;
    [SerializeField] public InventoryView sideInventoryView;
    [SerializeField] public GameObject mainInventory;
    private int sizeInventory;
    public int SizeInventory { get => sizeInventory; }
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private InteractorForObjects interactorForObjects;
    private Color color1 = new Color(255, 255, 255, 255);
    private Color color2 = new Color(255, 255, 255, 0);

    private void Awake()
    {
        try
        {
            inventoryItems = GetComponentsInChildren<InventoryItem>();
            sizeInventory = inventoryItems.Length;
            mainInventory.SetActive(false);

        }
        catch (System.Exception)
        {

        }

    }

    private void Start()
    {

    }


    private void OnEnable()
    {
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
        //mainCharacter.GetComponent<Player1>().enabled = false;
        inventoryItems = GetComponentsInChildren<InventoryItem>();

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
            item.image.color = color2;
            item.textMeshProUGUI.text = "";
            item.id = "";
        }
    }
}
