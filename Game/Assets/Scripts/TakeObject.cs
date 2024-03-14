using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class TakeObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameBox;
    [SerializeField] InventoryData inventoryData;
    [SerializeField] GameObject canvasBox;
    [SerializeField] public GameObject boxUIresource;
    Animator animator;
    public GameObject objInHand1;
    public BoxWithResource box;
    public GameObject objInHand2;
    Ray ray;
    [SerializeField] public Camera currentCamera;
    public float maxDistance = 2.0f;
    float force = 10f;
    public bool isTake = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        

        currentCamera = GetComponent<Player1>().currentCamera;
        ray = new Ray(currentCamera.transform.position, currentCamera.transform.forward);
        if (objInHand1 == null && isTake)
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
                    if (animator != null)
                    {
                        animator.SetTrigger("TakeObject");
                    }



                }

                if (hitInfo.collider.gameObject.layer == 11)
                {

                    box = hitInfo.collider.GetComponent<BoxWithResource>();
                    nameBox.text = box.name;

                    boxUIresource.GetComponent<InventoryView>().resource = box.resource;
                    canvasBox.gameObject.SetActive(true);
                    isTake = false;



                }
            }
        }
    }

    public void Drop()
    {
        if (objInHand1 != null)
        {

            objInHand1.transform.parent = null;
            objInHand1.GetComponent<Rigidbody>().isKinematic = false;

            objInHand1.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            objInHand1.GetComponent<MeshCollider>().enabled = true;
            objInHand1.GetComponent<Animator>().enabled = false;

            objInHand1.gameObject.layer = 3;

            Destroy(objInHand2);
            objInHand1 = null;
            isTake = true;
        }
    }
}