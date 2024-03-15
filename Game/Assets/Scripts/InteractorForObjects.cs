using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractorForObjects : MonoBehaviour
{
    private TakeObject takeObject;
    private Animator animator;
    private ScriptableItem[] items;
    private float time;
    private float timeUp = 0.7f;
    [SerializeField] InventoryView inventoryView;


    private void Start()
    {
        items = inventoryView.scriptableItems;
        animator = GetComponent<Animator>();
        takeObject = GetComponent<TakeObject>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= timeUp)
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
                                time = 0;
                                //takeObject.objInHand2.GetComponent<Animator>().SetTrigger("Chop");
                                if (item.id == "axe")
                                {
                                    animator.SetTrigger("Chop");
                                }
                                if (item.id == "pickaxe")
                                {
                                    animator.SetTrigger("PickAxe");
                                }

                                Ray ray = new Ray(takeObject.currentCamera.transform.position, takeObject.currentCamera.transform.forward);

                                if (Physics.Raycast(ray, out RaycastHit hit, takeObject.maxDistance))
                                {
                                    if (hit.collider.gameObject.layer == 8 && item.id == "axe")
                                    {
                                        Debug.Log("Chop");
                                        hit.collider.GetComponent<Tree>().Chop();

                                        if (hit.collider.GetComponent<Tree>().hp <= 0)
                                        {
                                            hit.collider.GetComponent<Tree>().hp = 20;
                                            hit.collider.GetComponent<Tree>().DeleteObject();
                                            if (inventoryView.inventoryData.inventory.ContainsKey(item.id))
                                            {
                                                inventoryView.inventoryData.RemoveData(item.id, 1);
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

                                    if (hit.collider.gameObject.layer == 9 && item.id == "pickaxe")
                                    {
                                        hit.collider.GetComponent<Stone>().Bam();
                                        
                                        if (hit.collider.GetComponent<Stone>().hp <= 0)
                                        {
                                            hit.collider.GetComponent<Stone>().hp = 20;
                                            hit.collider.GetComponent<Stone>().DeleteObject();
                                            if (inventoryView.inventoryData.inventory.ContainsKey(item.id))
                                            {
                                                inventoryView.inventoryData.RemoveData(item.id, 1);
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
}
