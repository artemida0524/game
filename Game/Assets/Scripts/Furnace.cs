using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{
    [SerializeField] public ParticleSystem[] particleSystemsFurnace;
    [NonSerialized] public ScriptableItemList scriptableItemList;

    [NonSerialized] public TextMeshProUGUI textMeshProUGUICountResource;
    [NonSerialized] public TextMeshProUGUI textMeshProUGUICountFuel;



    public float timeFuel = 0;
    public float timeResult = 0;
    //private bool isPlayParticle = false;
    private bool work = false;
    private bool isSelect = false;
    public bool isSetResource = false;

    [NonSerialized] public string idResource = "";
    [NonSerialized] public int countResource = 0;


    [NonSerialized] public string idFuel = "";
    [NonSerialized] public int countFuel = 0;

    [NonSerialized] public string idResult = "";
    [NonSerialized] public int countResult = 0;
    
    public void ForThrowForResult()
    {
        countResult = 0;
        idResult = "";
    }

    public void ForThrowForFuel()
    {
        countFuel = 0;
        idFuel = "";
    }

    public void ForThrowForResource()
    {
        countResource = 0;
        idResource = "";

        timeResult = 0;
        isSetResource = false;
    }

    private void Update()
    {


        if (work && countResource != 0/* && !isSetResource*/)
        {

            foreach (var item in scriptableItemList.scriptableItems)
            {
                if (idResource == item.id && countResource < item.countObjectForMerger)
                {
                    //work = false;

                    isSetResource = false;
                    break;
                    //foreach (var item2 in particleSystemsFurnace)
                    //{
                    //    item2.Stop();
                    //}
                }
                else
                {
                    isSetResource = true;
                }
            }
        }


        if (idFuel != "" && !work && countResource != 0)
        {
            foreach (var item in scriptableItemList.scriptableItems)
            {
                if (idResource == item.id && countResource >= item.countObjectForMerger)
                {
                    isSelect = true;
                    break;
                }
            }

            if (isSelect)
            {
                foreach (var item in scriptableItemList.scriptableItems)
                {

                    if (idFuel == item.id)
                    {
                        countFuel -= 1;
                        work = true;
                        timeFuel = item.secondFuel;

                        foreach (var item2 in particleSystemsFurnace)
                        {
                            item2.Play();
                        }
                    }
                }
                isSelect = false;
            }
        }

        if (work)
        {
            SetFuel();
        }

        if (isSetResource)
        {
            SetResource();
        }
    }

    private void SetFuel()
    {
        timeFuel -= Time.deltaTime;

        if(countFuel == 0)
        {
            idFuel = "";
        }

        if (timeFuel < 0)
        {
            work = false;
            timeFuel = 0;
            

            if (countFuel == 0)
            {
                idFuel = "";
            }

            foreach (var item in particleSystemsFurnace)
            {
                item.Stop();
            }
        }
    }

    private void SetResource()
    {

        //foreach (var item in scriptableItemList.scriptableItems)
        //{
        //    if (idResource == item.id && countResource < item.countObjectForMerger)
        //    {
        //        Debug.Log("BEMS");
        //        //work = false;

        //        isSetResource = false;
        //        //foreach (var item2 in particleSystemsFurnace)
        //        //{
        //        //    item2.Stop();
        //        //}
        //    }
        //}

        if (timeResult == 0)
        {
            foreach (var item in scriptableItemList.scriptableItems)
            {
                if (item.id == idResource)
                {
                    timeResult = item.secondFuseIfResource;
                }
            }
        }
        timeResult -= Time.deltaTime;

        if (timeFuel <= 0)
        {
            isSetResource = false;
        }

        if (timeResult < 0)
        {
            if (idResult == "")
            {
                foreach (var item in scriptableItemList.scriptableItems)
                {
                    if (item.id == idResource)
                    {
                        idResult = item.idResultIfResource;

                    }
                }
            }

            foreach (var item in scriptableItemList.scriptableItems)
            {
                if (idResource == item.id)
                {
                    countResource -= item.countObjectForMerger;

                    if (countResource == 0)
                    {
                        idResource = "";

                        //work = false;

                        //foreach (var item2 in particleSystemsFurnace)
                        //{
                        //    item2.Stop();
                        //}
                    }
                    else if(countResource < item.countObjectForMerger)
                    {
                        //work = false;

                        isSetResource = false;
                        //foreach (var item2 in particleSystemsFurnace)
                        //{
                        //    item2.Stop();
                        //}
                    }
                }
            }

            isSetResource = false;
            countResult += 1;
            timeResult = 0;
        }
    }
}