using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    private void Start()
    {
       gameObject.transform.parent = transform;
    }
    void Update()
    {
        
    }
}
