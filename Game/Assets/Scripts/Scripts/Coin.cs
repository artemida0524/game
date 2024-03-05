using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private GameObject objPrefab;

    void Start()
    {
        while (true)
        {
            Instantiate(objPrefab, new Vector3(Random.Range(-4.30f, 4.80f), 1.5f, Random.Range(-3.5f, 3.71f)), Quaternion.identity);
        }

    }

    
    void Update()
    {
        
    }
    

}
