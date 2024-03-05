using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWater : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 0, -100) * Time.deltaTime;

        if(transform.position.z <= -2069f)
        {
            transform.position = new Vector3(68.680450f, -28.75929f, 10902.01f);
        }
    }
}
