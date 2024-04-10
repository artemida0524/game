using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    private const string PATH_LOAD_SCENE = "loadScene";

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt(PATH_LOAD_SCENE));
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey(PATH_CURRENT_SCENE) && PlayerPrefs.GetInt(PATH_LOAD_SCENE) == 1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetString(PATH_CURRENT_SCENE));
            PlayerPrefs.SetInt(PATH_LOAD_SCENE, 0);
            
        }

    }

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);

        lastVisitesScene = PlayerPrefs.GetString(PATH_LAST_VISITED_SCENE);

    }

    private void Update()
    {

        Debug.Log(PlayerPrefs.GetInt(PATH_LOAD_SCENE));
        currentSceneName = SceneManager.GetActiveScene().name;

        Debug.Log(currentSceneName + " + " + lastVisitesScene);

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
        PlayerPrefs.SetInt(PATH_LOAD_SCENE, 1);
    }

}
