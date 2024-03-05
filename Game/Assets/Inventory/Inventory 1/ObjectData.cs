using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [SerializeField] public int count;
    [SerializeField] public string id;




    public ObjectData(int count, string id)
    {
        this.count = count;
        this.id = id;
    }

}