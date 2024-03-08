using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledTerrain : MonoBehaviour
{

    [SerializeField] private GameObject[] terrain;
    [SerializeField] GameObject mainObject;
    private void Update()
    {
        foreach (var terrains in terrain)
        {
            Vector3 direction = terrains.transform.position - mainObject.transform.position;

            float mag = direction.magnitude;

            if (mag < 700)
            {
                terrains.SetActive(true);
            }
            if (mag > 700)
            {
                terrains.SetActive(false);
            }
            
        }
    }
}
