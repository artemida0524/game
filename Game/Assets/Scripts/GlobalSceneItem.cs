using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneItem : MonoBehaviour
{
    public string nameScene;

    public GameObject canvasButton;
    public GameObject canvasGoingButton;


    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
        GameManagerGlobalScene.lastVisitesScene = "GlobalScene";
    }
}
