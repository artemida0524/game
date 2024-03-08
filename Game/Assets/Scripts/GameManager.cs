using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private GameObject mainMenu;
    private bool isActive = false;

    private void Update()
    {



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

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    File.WriteAllText(Application.persistentDataPath + "/mazafaka.json", "");
        //    inventoryData.inventory.Clear();
        //    PlayerPrefs.DeleteAll();
        //}
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