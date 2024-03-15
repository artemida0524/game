using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnGrass : MonoBehaviour
{
    [SerializeField] GameObject obj;
    public void TP()
    {
        Instantiate(obj, new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f)), Quaternion.identity);
        Destroy(obj);
        Debug.Log("instantiate");
    }
}
