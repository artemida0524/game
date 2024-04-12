using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.UI;


public class CanvasItemZoneView : MonoBehaviour
{
    private const int TIME_GO = 12;

    public GoingToTargetObject goingToTargetObject;
    private int timeSetter = 0;
    public GameObject selfCanvas;
    [SerializeField] InteractibleIndecatorGlobalScene interactibleIndecatorGlobalScene;
    public Image image;
    public InventoryItem[] inventoryItems;
    public GlobalCameraInteractible globalCameraInteractible;



    public TextMeshProUGUI levelHard;
    public TextMeshProUGUI runCount;
    public TextMeshProUGUI timeRun;
    public TextMeshProUGUI timeGo;

    public TextMeshProUGUI timer;
    private bool isTime = false;


    private int countLighting;
    private int countTimeLighting;

    private int countTimeGo;

    private void OnEnable()
    {
        globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isMove = false;
        globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isInteractable = false;
        image.sprite = globalCameraInteractible.lastCheckComponent.sprite;

        var list = globalCameraInteractible.lastCheckComponent.canFindList;

        for (int i = 0; i < list.Count; i++)
        {
            inventoryItems[i].gameObject.SetActive(true);
        }


        levelHard.text = $"Level Hard: {globalCameraInteractible.lastCheckComponent.levelHard}";
        int iterator = 0;

        foreach (var item in list)
        {
            foreach (var item2 in globalCameraInteractible.scriptableItemList.scriptableItems)
            {
                if (item == item2.id)
                {
                    inventoryItems[iterator].image.sprite = item2.sprite;
                    iterator++;
                }
            }
        }


        //Vector3 vector3 = globalCameraInteractible.currentCheckComponentPointer.transform.position - globalCameraInteractible.lastCheckComponent.pointer.transform.position;

        Vector3 vector3 = globalCameraInteractible.currentComponentSceneItem.pointer.transform.position - globalCameraInteractible.lastCheckComponent.pointer.transform.position;
        SetButtons(vector3.magnitude);

    }

    public void ExitButton(GameObject button)
    {
        button.SetActive(false);
    }


    //public void RunTo

    private void OnDisable()
    {
        globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isMove = true;
        globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isInteractable = true;

        foreach (var item in inventoryItems)
        {
            item.gameObject.SetActive(false);
        }

    }

    public void ClickRunButton()
    {

        if (int.Parse(interactibleIndecatorGlobalScene.indecatorItemLighting.count.text) >= countLighting)
        {
            interactibleIndecatorGlobalScene.indecatorItemLighting.count.text = $"{int.Parse(interactibleIndecatorGlobalScene.indecatorItemLighting.count.text) - countLighting}";

            interactibleIndecatorGlobalScene.timerCount = countTimeLighting;
            goingToTargetObject.timeToReachTarget = countTimeLighting;
            goingToTargetObject.target = globalCameraInteractible.lastCheckComponent.pointer.transform;

            interactibleIndecatorGlobalScene.isSetTimerGoing = true;
            globalCameraInteractible.camera.GetComponent<GlobalCameraMove>().isInteractable = false;



            ExitButton(selfCanvas);



        }
    }

    public void ClickGoButton()
    {

    }


    private void SetButtons(float distance)
    {
        countLighting = (int)distance / 10;

        countTimeLighting = countLighting;

        countTimeGo = countTimeLighting * TIME_GO;

        runCount.text = countLighting.ToString();
        timeRun.text = DataUpdater.TimeConvert(countTimeLighting);

        timeGo.text = DataUpdater.TimeConvert(countTimeGo);
    }



}
