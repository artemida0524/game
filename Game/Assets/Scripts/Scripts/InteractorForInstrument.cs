using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorForInstrument : MonoBehaviour
{
    [SerializeField] TakeObject takeObject;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (takeObject.objInhand != null)
            {
                takeObject.objInhand.GetComponent<Animator>().SetTrigger("push");
            }
        }
    }
}
