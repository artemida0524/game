using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin1 : MonoBehaviour
{
    float speed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {

        transform.Rotate(0, 0, 360.0f * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

}
