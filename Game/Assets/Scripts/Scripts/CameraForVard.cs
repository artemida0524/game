using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraForVard : MonoBehaviour
{
    [SerializeField] Camera camera;
    private Vector3 cameraForvard;
    float x;


    private void Update()
    {
        cameraForvard = camera.transform.forward;

        float xx = Input.GetAxis("Mouse X");
        float yy = Input.GetAxis("Mouse Y");
        x = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W))
        {
            cameraForvard.y = 0;
            transform.position += cameraForvard * Time.fixedDeltaTime;
        }

        Quaternion vector3 = camera.transform.rotation;
        vector3.x = 0;
        vector3.z = 0;

        transform.rotation = vector3;

    }
}
