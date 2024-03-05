using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CapsuleMove : MonoBehaviour
{
    [SerializeField] private GameObject circle2;
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject timeline1;
    [SerializeField] private GameObject timeline2;
    [SerializeField] private GameObject timeline3;
    [SerializeField] private GameObject timeline4;  
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject clown;
    [SerializeField] private GameObject morty;

    [SerializeField] public bool move = true;
    private float speed = 80f;
    private bool looked = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        if (move)
        {
            Controller();
        }
    }

    private void Controller()
    {
        //if (transform.position.y < 53.8f)
        //{
        //    transform.position = new Vector3(-10f, 71.85f, 5670.1f);
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(new Vector3(0, 0, 15) * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(new Vector3(0, 0, -15) * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(new Vector3(-15, 0, 0) * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(new Vector3(15, 0, 0) * Time.deltaTime);
        //}

        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * (speed+10f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * speed);

        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.right * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "triggerZone")
        {
            DetectCircle();
        }
        if (other.gameObject.name == "triggerZone2")
        {
            DetectCircle2();
            transform.position = new Vector3(384.48f, 297.07f, 696.94f);
        }
    }
    private void DetectCircle2()
    {
        circle2.SetActive(false);
        timeline3.GetComponent<PlayableDirector>().Play();

    }
    private void DetectCircle()
    {
        timeline2.SetActive(true);
        clown.GetComponent<Animator>().SetBool("talking", false);
        morty.GetComponent<Animator>().SetBool("talking", false);
        circle.SetActive(false);
        move = false;
    }

    public void GetCircle()
    {
        circle.SetActive(true);
        timeline1.SetActive(false);


    }
    public void SetMove()
    {
        move = !move;
    }
    private void Jump()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }



}
