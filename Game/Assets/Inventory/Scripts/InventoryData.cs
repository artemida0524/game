using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;




[Serializable]
public class InventoryDto
{
    public string id;
    public int count;

    public InventoryDto(string id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
[Serializable]
public class InventoryDtoList
{
    public InventoryDto[] data = new InventoryDto[10];
}


public class InventoryData : MonoBehaviour
{

    private const string INVENTORY_PATH = "Inventory";

    public Dictionary<string, ObjectData> inventory = new Dictionary<string, ObjectData>();

    [SerializeField] public List<GameObject> subjects = new List<GameObject>();
    [SerializeField] private InventoryView inventoryView;
    float timeOut = 0.5f;
    float time = 1f;


    private void Start()
    {
        if (PlayerPrefs.HasKey(INVENTORY_PATH))
        {

            string json = PlayerPrefs.GetString(INVENTORY_PATH);

            InventoryDtoList inventoryDtoList = JsonUtility.FromJson<InventoryDtoList>(json);

            foreach (var item in inventoryDtoList.data)
            {
                inventory[item.id] = new ObjectData(item.count, item.id);
            }




            File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", json);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeOut)
        {
            UpdateData();
            time = 0f;
        }
    }

    public void UpdateData()
    {
        InventoryDtoList list = new InventoryDtoList();
        list.data = new InventoryDto[inventory.Count];
        int iter = 0;
        foreach (var item in inventory)
        {
            list.data[iter] = new InventoryDto(item.Value.id, item.Value.count);
            iter++;

        }

        string json = JsonUtility.ToJson(list);
        PlayerPrefs.SetString(INVENTORY_PATH, json);
        File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", json);
    }



    public void AddData(string key, ObjectData data)
    {
        if (inventory.ContainsKey(key))
        {

            inventory[key].count += data.count;
        }
        else
        {
            if(inventory.Count < inventoryView.SizeInventory - 1)
            {

                inventory.Add(key, data);
            }
        }
    }

    public void AddData(string key, ObjectData data, GameObject obj)
    {
        if (inventory.ContainsKey(key))
        {
            Destroy(obj);
            inventory[key].count += data.count;
        }
        else
        {
            if (inventory.Count < inventoryView.SizeInventory - 1)
            {
                Destroy(obj);
                inventory.Add(key, data);
            }
        }
    }

    public void RemoveData(string key, int count)
    {
        if (inventory.ContainsKey(key))
        {
            if (inventory[key].count >= count)
            {
                inventory[key].count -= count;
            }

            if (inventory[key].count == 0)
            {
                inventory.Remove(key);
            }


            //InventoryDtoList list = new InventoryDtoList();
            //list.data = new InventoryDto[inventory.Count];
            //int iter = 0;
            //foreach (var item in inventory)
            //{
            //    list.data[iter] = new InventoryDto(item.Value.id, item.Value.count);
            //    iter++;

            //}

            //string json = JsonUtility.ToJson(list);
            //PlayerPrefs.SetString(INVENTORY_PATH, json);
            //File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", json);

        }
    }
}
