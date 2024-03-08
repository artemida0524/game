using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] public InventoryView inventoryView;
    [SerializeField] public GameObject target;
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
                    GameObject obj = Instantiate(item.gameObject, target.transform.position, target.transform.rotation);

                    obj.GetComponent<Rigidbody>().isKinematic = true;

                    obj.transform.parent = target.transform;

                    data.RemoveData(item.id, 1);


                    takeObject.objInhand = obj;

                    //takeObject.CursorEnable();

                    inventory.SetActive(false);
                }

            }
        }
    }
}
