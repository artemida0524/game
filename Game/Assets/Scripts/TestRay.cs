using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    Ray ray;
    [SerializeField] Camera camera;
    [SerializeField] private float maxDistance;
    void Start()
    {
        
        
    }

    void Update()
    {
        ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(camera.transform.position, camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Debug.Log(hitInfo.collider.name);
        }
    }
}
