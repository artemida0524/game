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
    float maxDistance = 10.0f;
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

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (inventoryData.inventory.ContainsKey("bear"))
                {
                    foreach (var item in inventoryData.subjects)
                    {
                        if (item.gameObject.GetComponent<ObjectData>().id == "bear")
                        {
                            Instantiate(item, hitInfo.point, Quaternion.identity);
                            inventoryData.RemoveData("bear", 1);
                        }

                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }

    }

    public void CursorEnable(bool enable)
    {
        //if(enable)
        //{
        //    Cursor.visible = enable;
        //    if(enable)
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //    }
        //    if(!enable)
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //}

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void RayTake()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
            {
                if (hitInfo.collider.gameObject.layer == 7)
                {
                    inventoryData.AddData(hitInfo.collider.GetComponent<ObjectData>().id, hitInfo.collider.GetComponent<ObjectData>());
                    Destroy(hitInfo.collider.gameObject);
                    if(animator != null)
                    {
                        animator.SetTrigger("TakeObject");
                    }
                    
                }
                if (hitInfo.collider.tag == "colliderTest")
                {
                    
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
