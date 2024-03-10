using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private InventoryData inventoryData;
    private ScriptableItem[] items;
    [SerializeField] InventoryView inventoryView;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TakeObject takeObject;
    public GameObject lastObject;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    private bool isActive = false;
    public bool viewObjectInHand = false;

    private void Start()
    {
        items = inventoryView.scriptableItems;
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", "");
        //    inventoryData.inventory.Clear();
        //    PlayerPrefs.DeleteAll();
        //}

        if (Input.GetKeyDown(KeyCode.E) && viewObjectInHand)
        {
            viewObjectInHand = !viewObjectInHand;
            Debug.Log("EEEEEE");
            if (takeObject.objInHand1 != null)
            {
                foreach (var item in items)
                {
                    if (item.id == takeObject.objInHand1.GetComponent<ObjectData>().id)
                    {
                        lastObject = item.gameObject;
                    }
                }

                inventoryData.AddData(takeObject.objInHand1.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInHand1.GetComponent<ObjectData>().id));

                Destroy(takeObject.objInHand1);

                Destroy(takeObject.objInHand2);

                takeObject.objInHand1 = null;
                takeObject.objInHand2 = null;

                takeObject.isTake = true;
                inventory.SetActive(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.R) && !viewObjectInHand)
        {
            Debug.Log("RRRRRR");
            viewObjectInHand = !viewObjectInHand;
            if (lastObject != null)
            {
                GameObject obj1 = Instantiate(lastObject, target1.transform.position, lastObject.gameObject.transform.rotation);
                GameObject obj2 = Instantiate(lastObject, target2.transform.position, Quaternion.identity);

                obj1.transform.parent = target1.transform;
                obj2.transform.parent = target2.transform;

                obj1.transform.localEulerAngles = new Vector3(1.2f, 256f, 167f);
                obj1.transform.localPosition = new Vector3(0.134f, 0.077f, 0.067f);
                obj2.transform.localEulerAngles = lastObject.gameObject.transform.localEulerAngles;

                obj1.GetComponent<Rigidbody>().isKinematic = true;

                obj1.GetComponent<MeshCollider>().enabled = false;

                obj2.GetComponent<Rigidbody>().isKinematic = true;
                obj2.GetComponent<MeshCollider>().enabled = false;

                obj1.gameObject.layer = 7;
                obj2.gameObject.layer = 6;

                inventoryData.RemoveData(lastObject.GetComponent<ObjectData>().id, 1);

                takeObject.objInHand1 = obj1;
                takeObject.objInHand2 = obj2;
                inventory.SetActive(false);
                takeObject.isTake = false;

            }
        }


        isActive = inventory.activeSelf;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isActive)
            {

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //mainCharacter.GetComponent<FPSController>().enabled = false;
            }
            if (isActive)
            {

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                //mainCharacter.GetComponent<FPSController>().enabled = true;
            }

            isActive = !isActive;
            inventory.SetActive(isActive);
        }


    }


    public void SetMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }
    public void OK()
    {
        mainMenu.gameObject.SetActive(false);
    }

}