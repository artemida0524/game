using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableItemList : MonoBehaviour
{
    public static ScriptableItemList instance;


    private void Awake()
    {
        instance = this;    
    }

    [SerializeField] public ScriptableItem[] scriptableItems;
}