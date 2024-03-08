using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
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

    private ScriptableItem[] items;


    private void Start()
    {
        items = inventoryView.scriptableItems;
    }

    public void GetObject()
    {
        if (takeObject.objInhand == null)
        {
            TakeInHand();

        }
        else
        {

            data.AddData(takeObject.objInhand.GetComponent<ObjectData>().id, new ObjectData(1, takeObject.objInhand.GetComponent<ObjectData>().id));

            Destroy(takeObject.objInhand);

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
                    GameObject obj2 = Instantiate(item.gameObject, target[1].transform.position, /*target[1].transform.rotation*/ item.gameObject.transform.rotation);



                    obj1.GetComponent<Rigidbody>().isKinematic = true;
                    obj1.GetComponent<MeshCollider>().enabled = false;

                    obj2.GetComponent<Rigidbody>().isKinematic = true;
                    obj2.GetComponent<MeshCollider>().enabled = false;


                    obj1.transform.parent = target[0].transform;

                    obj2.transform.parent = target[1].transform;
                    obj1.gameObject.layer = 0;

                    data.RemoveData(item.id, 1);

                    takeObject.objInhand = obj1;

                    inventory.SetActive(false);
                }

            }
        }
    }
}
