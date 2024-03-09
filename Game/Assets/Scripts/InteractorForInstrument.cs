using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorForInstrument : MonoBehaviour
{
    private TakeObject takeObject;
    private Animator animator;
    private ScriptableItem[] items;
    [SerializeField] InventoryView inventoryView;


    private void Start()
    {
        items = inventoryView.scriptableItems;
        animator = GetComponent<Animator>();
        takeObject = GetComponent<TakeObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (takeObject.objInHand1 != null)
            {
                foreach (var item in items)
                {
                    if (takeObject.objInHand1.GetComponent<ObjectData>().id == item.id)
                    {
                        if (item.typeObject == TypeObject.Instrument)
                        {

                            takeObject.objInHand1.GetComponent<Animator>().SetTrigger("push");
                            animator.SetTrigger("Chop");

                            Ray ray = new Ray(takeObject.currentCamera.transform.position, takeObject.transform.forward);

                            if (Physics.Raycast(ray, out RaycastHit hit, takeObject.maxDistance))
                            {
                                if (hit.collider.gameObject.layer == 8 && item.id == "axe")
                                {
                                    Debug.Log("Chop");
                                    hit.collider.GetComponent<Tree>().Chop();
                                    if (hit.collider.GetComponent<Tree>().hp <= 0)
                                    {
                                        if (inventoryView.inventoryData.inventory.ContainsKey(item.id))
                                        {
                                            inventoryView.inventoryData.RemoveData("axe", 1);
                                        }
                                        else
                                        {
                                            Destroy(takeObject.objInHand1);

                                            Destroy(takeObject.objInHand2);

                                            takeObject.objInHand1 = null;
                                            takeObject.objInHand2 = null;
                                            takeObject.isTake = true;
                                        }
                                        
                                    }
                                }
                            }

                        }

                        if (item.typeObject == TypeObject.Food)
                        {

                        }

                    }
                }

            }
        }
    }
}
