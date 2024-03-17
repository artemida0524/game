using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPhysicsForPlayer : MonoBehaviour
{

    public float floatUpSpeedLimit = 1.15f;
    public float floatUpSpeed = 4f;

    private void Start()
    {

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "WaterForPlayer")
        {
            
            float difference = (other.transform.position.y - transform.position.y) * floatUpSpeed;

            GetComponent<Rigidbody>().AddForce(new Vector3(0f, Mathf.Clamp((Mathf.Abs(Physics.gravity.y) * difference), 0, Mathf.Abs(Physics.gravity.y) * floatUpSpeedLimit), 0f), ForceMode.Acceleration);
            GetComponent<Rigidbody>().drag = 0.99f;
            GetComponent<Rigidbody>().angularDrag = 0.8f;
        }



    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "WaterForPlayer")
        {
            GetComponent<Rigidbody>().drag = 0f;
            GetComponent<Rigidbody>().angularDrag = 0f;
        }
    }
}