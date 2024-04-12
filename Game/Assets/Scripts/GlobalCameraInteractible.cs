using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCameraInteractible : MonoBehaviour
{
    private Ray ray;
    public Camera camera;

    [SerializeField] public ScriptableItemList scriptableItemList;

    [SerializeField] private GameObject canvasItemZone;

    [NonSerialized] public GlobalSceneItem lastCheckComponent;
    [NonSerialized] public GlobalSceneItem currentComponentSceneItem;
    



    private void Start()
    {
        camera = GetComponent<Camera>();



    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            Debug.Log(lastCheckComponent.pointer.name + " + " + currentComponentSceneItem.pointer.name);

        }
        catch (Exception)
        {

        }

        if (Input.GetMouseButtonDown(0) && camera.GetComponent<GlobalCameraMove>().isInteractable)
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<GlobalSceneItem>(out GlobalSceneItem component))
                {
                    Debug.Log("item");
                    if (component.nameScene == GameManagerGlobalScene.lastVisitesScene)
                    {

                        if (lastCheckComponent != null)
                        {
                            lastCheckComponent.canvasButton.SetActive(false);
                        }

                        component.canvasButton.SetActive(true);
                        lastCheckComponent = component;
                    }
                    else
                    {
                        if (lastCheckComponent != null)
                        {
                            lastCheckComponent.canvasButton.SetActive(false);

                        }
                        lastCheckComponent = component;

                        canvasItemZone.SetActive(true);
                    }
                }

            }
        }

    }
}
