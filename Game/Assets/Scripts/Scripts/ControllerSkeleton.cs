using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSkeleton : MonoBehaviour
{
    private float speed = 10f;
    float xRotate;
    [SerializeField] private GameObject skeletonHead;
    [SerializeField] private GameObject targetObject;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetObject.transform.position = skeletonHead.transform.position;
    }
}
