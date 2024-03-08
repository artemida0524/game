using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public void AddData(string key, ObjectData data)
    {
        if (inventory.ContainsKey(key))
        {

            inventory[key].count += data.count;



            InventoryDtoList list = new InventoryDtoList();
            list.data = new InventoryDto[inventory.Count];
            int iter = 0;

            foreach (var item in inventory)
            {

                list.data[iter] = new InventoryDto(item.Value.id, item.Value.count);
                iter++;
                string json = JsonUtility.ToJson(list);


                PlayerPrefs.SetString(INVENTORY_PATH, json);
                File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", json);


            }

        }
        else
        {
            inventory.Add(key, data);
            

            InventoryDtoList list = new InventoryDtoList();
            list.data = new InventoryDto[inventory.Count];

            int iter = 0;

            foreach (var item in inventory)
            {

                list.data[iter] = new InventoryDto(item.Value.id, item.Value.count);
                iter++;
                string json = JsonUtility.ToJson(list);


                PlayerPrefs.SetString(INVENTORY_PATH, json);

                File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", json);

            }


        }
    }


    public void RemoveData(string key, int count)
    {
        if (inventory.ContainsKey(key))
        {
            if(inventory[key].count >= count)
            {
                inventory[key].count -= count;
            }

            if (inventory[key].count == 0)
            {
                inventory.Remove(key);
            }


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
    }
}
