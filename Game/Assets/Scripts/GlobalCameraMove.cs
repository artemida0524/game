using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GlobalCameraMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Ray ray;
    private Camera camera;

    private GlobalSceneItem lastCheckComponent;

    private void Start()
    {
        camera = GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody>();
    }
    private float speed = 10000f;

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<GlobalSceneItem>(out GlobalSceneItem component))
                {
                    Debug.Log("item");
                    if (component.nameScene == GameManagerGlobalScene.lastVisitesScene)
                    {
                        component.canvasButton.SetActive(true);
                        lastCheckComponent = component;
                    }
                    else
                    {
                        component.canvasGoingButton.SetActive(true);
                    }
                }
                
            }
        }

    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.AddForce(new Vector3(0, 0, speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(new Vector3(0, 0, -speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.AddForce(new Vector3(-speed, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(new Vector3(speed, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 17000;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 10000;
        }
    }
}
