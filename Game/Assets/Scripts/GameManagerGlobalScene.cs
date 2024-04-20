using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerGlobalScene : MonoBehaviour
{
    public static GameManagerGlobalScene instance { get; private set; }
    public static string currentSceneName;
    public static string lastVisitesScene;

    public const string PATH_CURRENT_SCENE = "currentScene";
    public const string PATH_LAST_VISITED_SCENE = "lastVisitedScene";
    private const string PATH_LOAD_SCENE_BOOL = "loadScene";

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey(PATH_CURRENT_SCENE) && PlayerPrefs.GetInt(PATH_LOAD_SCENE_BOOL) == 1)
        {


            if (PlayerPrefs.GetString(PATH_CURRENT_SCENE) != scene.name)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString(PATH_CURRENT_SCENE));
            }

            PlayerPrefs.SetInt(PATH_LOAD_SCENE_BOOL, 0);
        }
        lastVisitesScene = PlayerPrefs.GetString(PATH_LAST_VISITED_SCENE);



    }


    private void Start()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "GlobalScene")
        //{
        //    Player1.Instance.gameObject.SetActive(false);
        //}
        //else
        //{

        //    Player1.Instance.gameObject.SetActive(true);

        //}

        Debug.Log("Startgamemanagerglobalscene");
        PlaceInTheWorld.Instance.SetPlace();
        
        Player1.SetPlaceInWorldPlayer();
    }



    private void Update()
    {

        currentSceneName = SceneManager.GetActiveScene().name;

        

        PlayerPrefs.SetString(PATH_CURRENT_SCENE, currentSceneName);
        PlayerPrefs.SetString(PATH_LAST_VISITED_SCENE, lastVisitesScene);

        if (currentSceneName == "GlobalScene")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PATH_LOAD_SCENE_BOOL, 1);
    }

}
