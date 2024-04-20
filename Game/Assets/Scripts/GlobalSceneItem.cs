using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum LevelHard
{
    none,
    easy,
    medium,
    hard,
    impossible
}

public class GlobalSceneItem : MonoBehaviour
{
    public string nameScene;

    public GameObject canvasButton;
    public GameObject pointer;
    [SerializeField] private GlobalCameraInteractible globalCameraInteractible;
    


    public List<string> canFindList = new List<string>();
    public Sprite sprite;
    public LevelHard levelHard;

    private void Awake()
    {
        if (nameScene == GameManagerGlobalScene.lastVisitesScene)
            globalCameraInteractible.currentComponentSceneItem = this;

    }

    private void Update()
    {
        if (nameScene == GameManagerGlobalScene.lastVisitesScene)
        {
            globalCameraInteractible.currentComponentSceneItem = this;
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nameScene);
        GameManagerGlobalScene.lastVisitesScene = "GlobalScene";
    }
}
