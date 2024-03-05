using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpScene : MonoBehaviour
{
    public string mainScene = "Terrain";
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "TpPressToMainScene")
        {
            SceneManager.LoadScene("Terrain");
        }
        if(other.gameObject.name == "TpMainSceneToPress")
        {
            SceneManager.LoadScene("Press");
        }
    }
}
