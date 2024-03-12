using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Obj
{
    int count;
    string name;
}

public class BoxWithResource : MonoBehaviour
{

    [SerializeField] string[] nameObj;
    [SerializeField] int[] countObj;

    public Dictionary<string, ObjectData> resource = new Dictionary<string, ObjectData>(12);

    public string name;

    private void Start()
    {

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
}
