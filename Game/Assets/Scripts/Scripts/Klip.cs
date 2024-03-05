using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class Klip : MonoBehaviour
{
    [SerializeField] GameObject circle2;
    [SerializeField] GameObject mainObject;
    [SerializeField] GameObject clown;
    [SerializeField] GameObject morty;
    [SerializeField] GameObject donald;
    [SerializeField] GameObject lookObject;
    [SerializeField] GameObject door;
    [SerializeField] GameObject black;
    [SerializeField] GameObject timeline4;

    private bool lookToMainObjectFromDonald = false;
    private bool lookToMainObjectFromClown = false;
    private bool lookToMainObjectFromMorty = false;
    private bool booltime = false;
    private bool moveClown = false;
    private bool moveMorty = false;
    public bool isEclipseReverse = false;
    public bool isEclipse = false;

    private void Start()
    {
        mainObject.transform.Rotate(new Vector3(0f, transform.rotation.y + 90f, 0f));
    }
    private void Update()
    {
        if (lookToMainObjectFromDonald)
        {
            LookToMainObjectFromDonald();
        }

        if (booltime)
        {
            transform.Translate(new Vector3(0, 0, -5f) * Time.deltaTime);
        }

        if (lookToMainObjectFromClown)
        {
            LookToMainObjectFromClown();
        }

        if (lookToMainObjectFromMorty)
        {
            LookToMainObjectFromMorty();
        }

        if (moveClown)
        {
            MoveClown();
        }

        if (moveMorty)
        {
            MoveMorty();
        }

        if (isEclipseReverse)
        {
            black.GetComponent<Dark>().Timeline4();
            if (black.GetComponent<Dark>().color.a <= 0)
            {

                isEclipseReverse = false;
            }
        }

        if (isEclipse)
        {

        }

    }

    public void SetBoolTime()
    {
        booltime = !booltime;
    }
    public void SetBoolLookToMainObjectFromDonald()
    {
        lookToMainObjectFromDonald = !lookToMainObjectFromDonald;
    }

    private void LookToMainObjectFromDonald()
    {
        Vector3 direction = mainObject.transform.position - donald.transform.position;
        direction.y = 0f;
        donald.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void LookToMainObjectFromMorty()
    {

        Vector3 directionMorty = mainObject.transform.position - morty.transform.position;

        directionMorty.y = 0f;

        morty.transform.rotation = Quaternion.LookRotation(directionMorty);
    }

    public void LookToMainObjectFromClown()
    {
        Vector3 directionClown = mainObject.transform.position - clown.transform.position;
        directionClown.y = 0f;
        clown.transform.rotation = Quaternion.LookRotation(directionClown);
    }

    public void LookAndRotationClown()
    {
        Vector3 direction = lookObject.transform.position - clown.transform.position;

        clown.transform.rotation = Quaternion.LookRotation(direction);

        clown.GetComponent<Animator>().SetBool("walk", true);
        moveClown = true;
    }
    public void SetTimeLine4()
    {
        timeline4.GetComponent<PlayableDirector>().Play();
        mainObject.transform.position = new Vector3(384.73f, 301.10f, 703.95f);
    }
    public void LookAndRotationMorty()
    {
        Vector3 direction = lookObject.transform.position - morty.transform.position;
        morty.transform.rotation = Quaternion.LookRotation(direction);
        morty.GetComponent<Animator>().SetBool("walk", true);
        moveMorty = true;
        mainObject.GetComponent<CapsuleMove>().move = true;
    }

    public void MoveClown()
    {
        clown.transform.Translate(new Vector3(0f, 0f, 6f) * Time.deltaTime);
    }
    public void MoveMorty()
    {
        morty.transform.Translate(new Vector3(0f, 0f, 6f) * Time.deltaTime);
    }
    public void OpenDoor()
    {
        door.GetComponent<Animator>().SetTrigger("opendoor");
    }

    public void InvokeEnabledCircle2()
    {
        Invoke("EnabledCircle2", 8);
    }
    public void EnabledCircle2()
    {
        circle2.SetActive(true);
    }
    public void InvokeOpenDoor()
    {
        Invoke("OpenDoor", 2.5f);
    }
    public void SetIsEclipse()
    {
        black.GetComponent<Dark>().isEclipse = true;
    }
    public void SetIsEclipseReverse()
    {
        isEclipseReverse = true;
    }
    public void SetIsEclipse2()
    {
        isEclipse = true;
    }
}