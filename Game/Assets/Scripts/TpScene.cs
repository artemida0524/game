using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TpScene : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (other.tag == "Player")
        {

            if (scene.name != "MainScene")
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(1);
            }

        }


    }

}
