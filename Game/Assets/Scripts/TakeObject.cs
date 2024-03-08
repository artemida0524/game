using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class TakeObject : MonoBehaviour
{
    [SerializeField] InventoryData inventoryData;
    Animator animator;
    [SerializeField] GameObject target;
    public GameObject objInhand;
    Ray ray;
    [SerializeField] public Camera mainCamera;
    float maxDistance = 5.0f;
    float force = 10f;
    public bool isTake = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        if (objInhand == null)
        {
            RayTake();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }

    }


    private void RayTake()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
            {
                if (hitInfo.collider.gameObject.layer == 3)
                {
                    inventoryData.AddData(hitInfo.collider.GetComponent<ObjectData>().id, hitInfo.collider.GetComponent<ObjectData>());
                    Destroy(hitInfo.collider.gameObject);
                    if(animator != null)
                    {
                        animator.SetTrigger("TakeObject");
                    }
                    
                }
            }
        }
    }

    public void Drop()
    {
        if(objInhand != null)
        {
            objInhand.transform.parent = null;
            objInhand.GetComponent<Rigidbody>().isKinematic = false;

            objInhand.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            objInhand = null;
        }
    }
}