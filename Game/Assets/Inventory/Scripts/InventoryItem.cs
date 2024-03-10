using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] public InventoryView inventoryView;
    [SerializeField] public GameObject[] target;
    [SerializeField] public GameObject inventory;
    [SerializeField] public InventoryData data;
    [SerializeField] public TakeObject takeObject;
    [SerializeField] private GameManager gameManager;

    private ScriptableItem[] items;

    private void Start()
    {
        items = inventoryView.scriptableItems;
    }

    public void GetObject()
    {   
        if (textMeshProUGUI.text == "")
        {
            if (takeObject.objInHand1 != null)
            {
                data.AddData(takeObject.objInHand1.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInHand1.GetComponent<ObjectData>().id));

                Destroy(takeObject.objInHand1);

                Destroy(takeObject.objInHand2);

                takeObject.objInHand1 = null;
                takeObject.objInHand2 = null;
                takeObject.isTake = true;
                inventory.SetActive(false);
                gameManager.lastObject = null;
                gameManager.viewObjectInHand = false;
            }
        }
        

        if (takeObject.objInHand1 == null)
        {
            TakeInHand();

        }
        else
        {

            data.AddData(takeObject.objInHand1.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInHand1.GetComponent<ObjectData>().id));

            Destroy(takeObject.objInHand1);
            Destroy(takeObject.objInHand2);

            TakeInHand();

        }
    }

    private void TakeInHand()
    {
        foreach (var item in items)
        {

            if (item.sprite == image.sprite)
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
                    gameManager.viewObjectInHand = true;


                    obj2.GetComponent<Rigidbody>().isKinematic = true;
                    obj2.GetComponent<MeshCollider>().enabled = false;

                    obj1.gameObject.layer = 7;
                    obj2.gameObject.layer = 6;

                    data.RemoveData(item.id, 1);

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

                    obj1.GetComponent<MeshCollider>().enabled = false;

                    obj2.GetComponent<Rigidbody>().isKinematic = true;
                    obj2.GetComponent<MeshCollider>().enabled = false;

                    obj1.gameObject.layer = 7;
                    obj2.gameObject.layer = 6;

                    data.RemoveData(item.id, 1);

                    takeObject.objInHand1 = obj1;
                    takeObject.objInHand2 = obj2;
                    inventory.SetActive(false);
                    takeObject.isTake = false;
                }
            }
        }
    }
}
