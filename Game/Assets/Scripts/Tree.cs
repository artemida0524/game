using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class Tree : MonoBehaviour
{

    [SerializeField] private float time;
    private float timeUp = 0;
    [SerializeField] GameObject obj;
    private void Update()
    {
        timeUp += Time.deltaTime;
        if (timeUp >= time)
        {
            Vector3 vector3 = transform.position;
            vector3.x += Random.Range(-4.0f, 4.0f);
            vector3.y += Random.Range(3f, 6.0f);
            vector3.z += Random.Range(-4.0f, 4.0f);
            timeUp = 0;
            GameObject app =  Instantiate(obj, vector3, Quaternion.identity);

            

        }
    }
}
