using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] public string id;
    [SerializeField] public InventoryView inventoryView;
    [SerializeField] public InventoryView mainInventoryView;
    [SerializeField] public InventoryView boxInventoryView;
    [SerializeField] public GameObject[] target;
    [SerializeField] public GameObject inventory;
    [SerializeField] public InventoryData inventoryData;
    [SerializeField] public TakeObject takeObject;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Player1 mainPlayer;

    private ScriptableItem[] items;

    private void Start()
    {
        items = inventoryView.scriptableItemList.scriptableItems;
    }

    public void ThrowInBox()
    {
        Debug.Log(boxInventoryView.resource.Count  + " " + boxInventoryView.SizeInventory);

        foreach (var item in inventoryView.sideInventoryView.inventoryItems)
        {

            if (boxInventoryView.resource.Count == boxInventoryView.SizeInventory)
            {
                Debug.Log("full");
            }

            if (item.GetComponent<InventoryItem>().id == "")
            {
                takeObject.box.resource.Add(id, new ObjectData(int.Parse(textMeshProUGUI.text), id));
                inventoryData.RemoveData(id, int.Parse(textMeshProUGUI.text));
                break;
            }
            if (item.GetComponent<InventoryItem>().id == id)
            {
                takeObject.box.resource[id] = new ObjectData(int.Parse(item.GetComponent<InventoryItem>().textMeshProUGUI.text) + int.Parse(textMeshProUGUI.text), id);
                inventoryData.RemoveData(id, int.Parse(textMeshProUGUI.text));
                break;
            }

        }

        inventory.SetActive(false);
        Debug.Log("enabled");
        inventory.SetActive(true);

    }

    public void ThrowInInventory()
    {
        if (mainInventoryView.inventoryData.inventory.Count == mainInventoryView.SizeInventory - 1)
        {
            foreach (var item in inventoryView.sideInventoryView.inventoryItems)
            {
                if (item.GetComponent<InventoryItem>().id == id)
                {
                    inventoryData.inventory[id] = new ObjectData(int.Parse(textMeshProUGUI.text) + int.Parse(item.textMeshProUGUI.text), id);
                    takeObject.box.resource.Remove(id);
                    Debug.Log(item.GetComponent<InventoryItem>().id);

                    break;
                }
            }
        }
        else
        {
            foreach (var item in inventoryView.sideInventoryView.inventoryItems)
            {
                if (item.GetComponent<InventoryItem>().id == "")
                {
                    inventoryData.AddData(id, new ObjectData(int.Parse(textMeshProUGUI.text), id));
                    takeObject.box.resource.Remove(id);
                    break;
                }

                if (item.GetComponent<InventoryItem>().id == id)
                {
                    inventoryData.inventory[id] = new ObjectData(int.Parse(textMeshProUGUI.text) + int.Parse(item.textMeshProUGUI.text), id);
                    takeObject.box.resource.Remove(id);
                    Debug.Log(item.GetComponent<InventoryItem>().id);

                    break;
                }
            }

        }


        inventory.SetActive(false);
        inventory.SetActive(true);

    }

    public void GetObject()
    {
        if (textMeshProUGUI.text == "")
        {
            if (takeObject.objInHand1 != null)
            {
                inventoryData.AddData(takeObject.objInHand1.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInHand1.GetComponent<ObjectData>().id));

                Destroy(takeObject.objInHand1);

                Destroy(takeObject.objInHand2);

                takeObject.objInHand1 = null;
                takeObject.objInHand2 = null;
                takeObject.isTake = true;
                inventory.SetActive(false);
                gameManager.lastObject = null;
                gameManager.viewObjectInHand = false;
                return;
            }
        }


        if (takeObject.objInHand1 == null)
        {
            TakeInHand();

        }
        else
        {

            inventoryData.AddData(takeObject.objInHand1.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInHand1.GetComponent<ObjectData>().id));

            Destroy(takeObject.objInHand1);
            Destroy(takeObject.objInHand2);

            TakeInHand();

        }

        foreach (var item in items)
        {
            if (item.id == id && item.typeObject == TypeObject.Build)
            {
                //objectBuild = item.gameObject;
                //takeObject.isTake = false;
                //Debug.Log(item.id);
                mainPlayer.Build(item.gameObject);


                //inventory.SetActive(false);
            }
        }
        //Ray ray = new Ray(currentCamera.transform.position, currentCamera.transform.forward);

        //if (Input.GetKeyDown(KeyCode.I) && !booltest)
        //{
        //    booltest = true;
        //    objTest = Instantiate(objTest, new Vector3(0, 0, 0), objTest.transform.rotation);
        //    objTest.GetComponent<BoxCollider>().enabled = false;
        //    takeObject.isTake = false;
        //}


        //if (Physics.Raycast(ray, out RaycastHit hitInfo, takeObject.maxDistance) && booltest)
        //{
        //    objTest.transform.position = hitInfo.point;
        //    objTest.transform.position += new Vector3(0, 0.4f, 0);

        //    if (Input.GetKey(KeyCode.Z))
        //    {
        //        objTest.transform.Rotate(new Vector3(0, 0, -40) * Time.deltaTime);
        //    }
        //    if (Input.GetKey(KeyCode.X))
        //    {
        //        objTest.transform.Rotate(new Vector3(0, 0, 40) * Time.deltaTime);
        //    }
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        booltest = false;
        //        objTest.GetComponent<BoxCollider>().enabled = true;
        //        takeObject.isTake = true;
        //    }
        //}
    }

    private void TakeInHand()
    {
        foreach (var item in items)
        {

            if (item.id == id)
            {
                if (item.typeObject == TypeObject.Instrument)
                {
                    GameObject obj1 = Instantiate(item.gameObject, target[0].transform.position, item.gameObject.transform.rotation);
                    GameObject obj2 = Instantiate(item.gameObject, target[1].transform.position, Quaternion.identity);

                    obj1.transform.parent = target[0].transform;
                    obj2.transform.parent = target[1].transform;

                    obj1.transform.localEulerAngles = new Vector3(1.2f, 256f, 167f);
                    obj1.transform.localPosition = new Vector3(0.134f, 0.077f, 0.067f);
                    obj2.transform.localEulerAngles = item.gameObject.transform.localEulerAngles;

                    obj1.GetComponent<Rigidbody>().isKinematic = true;

                    obj1.GetComponent<MeshCollider>().enabled = false;

                    gameManager.lastObject = item.gameObject;
                    gameManager.viewObjectInHand = false;


                    obj2.GetComponent<Rigidbody>().isKinematic = true;
                    obj2.GetComponent<MeshCollider>().enabled = false;

                    obj1.gameObject.layer = 7;
                    obj2.gameObject.layer = 6;

                    inventoryData.RemoveData(item.id, 1);

                    takeObject.objInHand1 = obj1;
                    takeObject.objInHand2 = obj2;
                    inventory.SetActive(false);
                    takeObject.isTake = false;
                }

                if (item.typeObject == TypeObject.Food)
                {
                    GameObject obj1 = Instantiate(item.gameObject, target[0].transform.position, item.gameObject.transform.rotation);
                    GameObject obj2 = Instantiate(item.gameObject, target[1].transform.position, Quaternion.identity);

                    obj1.transform.parent = target[0].transform;
                    obj2.transform.parent = target[1].transform;


                    obj1.transform.localEulerAngles = new Vector3(1.2f, 256f, 167f);
                    obj1.transform.localPosition = new Vector3(0.134f, 0.077f, 0.067f);
                    obj2.transform.localEulerAngles = item.gameObject.transform.localEulerAngles;

                    obj1.GetComponent<Rigidbody>().isKinematic = true;
                    gameManager.lastObject = item.gameObject;
                    gameManager.viewObjectInHand = false;
                    obj1.GetComponent<MeshCollider>().enabled = false;

                    obj2.GetComponent<Rigidbody>().isKinematic = true;
                    obj2.GetComponent<MeshCollider>().enabled = false;

                    obj1.gameObject.layer = 7;
                    obj2.gameObject.layer = 6;

                    inventoryData.RemoveData(item.id, 1);

                    takeObject.objInHand1 = obj1;
                    takeObject.objInHand2 = obj2;
                    inventory.SetActive(false);
                    takeObject.isTake = false;
                }
            }
        }
    }
}