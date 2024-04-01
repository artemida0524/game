using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField] private GameObject miniMapCamera;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject bigStone;
    [SerializeField] private GameObject bigIronOre;
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject fiber;
    [SerializeField] private GameObject wood;


    [SerializeField] private GameObject inventory;
    [SerializeField] private InventoryData inventoryData;
    private ScriptableItem[] items;
    [SerializeField] InventoryView inventoryView;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject canvasBox;
    [SerializeField] private TakeObject takeObject;
    [SerializeField] private GameObject craftCanvas;
    [SerializeField] private GameObject FurnaceCanvas;
    public GameObject lastObject;
    [SerializeField] private GameObject target1;
    [SerializeField] private GameObject target2;
    private bool isActive = false;
    public bool viewObjectInHand = false;
    private Vector3 newPosition;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            instance = this;
        }
    }

    private void Start()
    {
        items = inventoryView.scriptableItemList.scriptableItems;

        for (int i = 0; i < 20; i++)
        {
            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(tree, newPosition, Quaternion.identity);


            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(bigStone, newPosition, Quaternion.identity);

            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(fiber, newPosition, Quaternion.identity);

            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(bigIronOre, newPosition, Quaternion.identity);

        }
        for (int i = 0; i < 50; i++)
        {
            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(wood, newPosition, Quaternion.identity);

            newPosition = new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f));
            Instantiate(stone, newPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (lastObject != null)
        {
            Debug.Log( "last"+ " " + lastObject.name);

        }
        if (takeObject.objInHand1 != null)
        {
            Debug.Log("obj" + " " + takeObject.objInHand1.name);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenu.activeSelf == false)
            {
                mainMenu.SetActive(true);
                takeObject.isTake = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                mainMenu.SetActive(false);
                takeObject.isTake = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", "");
        //    inventoryData.inventory.Clear();
        //    PlayerPrefs.DeleteKey("Inventory");
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftCanvas.activeSelf == false)
            {
                craftCanvas.SetActive(true);
                takeObject.isTake = false;

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                craftCanvas.SetActive(false);
                takeObject.isTake = true;

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (canvasBox.activeSelf == true)
            {
                canvasBox.SetActive(false);

                takeObject.isTake = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)/* && viewObjectInHand*/)
        {
            Debug.Log(takeObject.objInHand1.name);
            if (takeObject.objInHand1 != null)
            {
                viewObjectInHand = !viewObjectInHand;
                Debug.Log("EEEEEE");
                foreach (var item in items)
                {
                    if (item.id == takeObject.objInHand1.GetComponent<ObjectData>().id)
                    {
                        Debug.Log(item.id);
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

        if (Input.GetKeyDown(KeyCode.R) && viewObjectInHand)
        {
            Debug.Log("r");
            if (lastObject != null)
            {
                viewObjectInHand = !viewObjectInHand;
                Debug.Log("RRRRRR");
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

    public void Take()
    {
        takeObject.isTake = true;
    }


    public void SetMenu()
    {
        mainMenu.gameObject.SetActive(true);
    }
    public void OK(GameObject canvas)
    {
        canvas.gameObject.SetActive(false);

        takeObject.isTake = true; 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ForCanvasFurnace()
    {
        FurnaceCanvas.SetActive(false);
        FurnaceCanvas.SetActive(true);
    }

    public void EnabledCanvas(GameObject canvas)
    {
        canvas.SetActive(!canvas.activeSelf);
    }
    
}