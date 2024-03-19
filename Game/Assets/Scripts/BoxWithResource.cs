using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class BoxWithResource : MonoBehaviour
{

    private float timeOut = 0.5f;
    private float time;

    [SerializeField] string[] nameObj;
    [SerializeField] int[] countObj;

    public string name;

    public Dictionary<string, ObjectData> resource = new Dictionary<string, ObjectData>(12);

    [SerializeField] public string idPlayerPrefs = "";

    private static int id = 0;
        
    private void Start()
    {
        if(idPlayerPrefs == "")
        {
            id++;
            idPlayerPrefs = $"box {id}";
        }


        if (PlayerPrefs.HasKey(idPlayerPrefs))
        {
            string json = PlayerPrefs.GetString(idPlayerPrefs);
            InventoryDtoList a = JsonUtility.FromJson<InventoryDtoList>(json);
            foreach (var item in a.data)
            {
                resource.Add(item.id, new ObjectData(item.count, item.id));
            }
        }

        Debug.Log(idPlayerPrefs);
        

        if (nameObj.Length != 0 && countObj.Length != 0)
        {
            if(nameObj.Length == countObj.Length)
            {
                for (int i = 0; i < nameObj.Length; i++)
                {
                    resource.Add(nameObj[i], new ObjectData(countObj[i], nameObj[i]));
                }
            }
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time > timeOut)
        {
            UpdateData();
            time = 0;
        }
    }

    private void UpdateData()
    {
        InventoryDtoList dtoList = new InventoryDtoList();

        dtoList.data = new InventoryDto[resource.Count];

        int iter = 0;
        foreach (var item in resource)
        {
            dtoList.data[iter] = new InventoryDto(item.Value.id, item.Value.count);
            iter++;

        }

        string json = JsonUtility.ToJson(dtoList);
        PlayerPrefs.SetString(idPlayerPrefs, json);
    }
}
