using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryItem inventoryItem;
    private ScriptableItemList scriptableItemList;
    public bool lead = false;
    public float time = 0f;
    private float timeOut = 1f;


    private void Start()
    {
        inventoryItem = GetComponent<InventoryItem>();
    }

    private void Update()
    {

        if (lead)
        {
            time += Time.deltaTime;
        }
        if (time > timeOut && lead && inventoryItem.id != string.Empty)
        {
            foreach (var item in inventoryItem.inventoryView.scriptableItemList.scriptableItems)
            {
                if (inventoryItem.id == item.id)
                {
                    inventoryItem.descriptionText.text = item.description;
                    inventoryItem.descriptionName.text = item.id;
                }
            }
            inventoryItem.descriptionPanel.SetActive(true);
        }
        else
        {
            inventoryItem.descriptionPanel.SetActive(false);
            inventoryItem.descriptionText.text = string.Empty;
            inventoryItem.descriptionName.text = string.Empty;
        }
    }




    public void OnPointerExit(PointerEventData pointerEventData)
    {

        lead = false;
        time = 0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        lead = true;
    }

}
