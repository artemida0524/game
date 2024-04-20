using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class PlaceInTheWorld : MonoBehaviour
{
    public ScriptableSceneItemList sceneItemList;
    public static PlaceInTheWorld Instance { get; private set; }

    [NonSerialized] public Vector3 place;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
            Instance = this;

        }
    }


    private void Start()
    {

        Scene scene = SceneManager.GetActiveScene();



        if (scene.name != "GlobalScene")
        {

            foreach (var item in sceneItemList.scriptableSceneItems)
            {
                if (item.idScene == scene.name)
                {

                    place = item.scenePlace;

                    Player1.Instance.gameObject.transform.position = place;

                }
            }

        }


    }

    public void SetPlace()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        foreach (var item in sceneItemList.scriptableSceneItems)
        {
            if (item.idScene == scene.name)
            {

                place = item.scenePlace;

                Player1.Instance.gameObject.transform.position = place;

            }
        }
        try
        {
        Player1.Instance.gameObject.transform.position = place;

        }
        catch (Exception)
        {

            
        }
    }

}
