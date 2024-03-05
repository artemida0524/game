using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private bool isRotate = true;

    void Start()
    {

    }

    void Update()
    {
        if(transform.position.y<= -10.47f) {

            transform.position = new Vector3(15.6f, 17.26f, -3.49f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!isRotate)
            {
                animator.SetTrigger("isRotate");
                isRotate =!isRotate;    
            }
            animator.SetBool("isBool", true);


            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * speed;
        }

        else if (Input.GetKey(KeyCode.D))
        {

            if (isRotate)
            {
                animator.SetTrigger("isRotate");
                isRotate = !isRotate;

            }
           

            animator.SetBool("isBool", true);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * speed;

        }

        else
        {
            animator.SetBool("isBool", false);
        }


    }

    private void Jump()
    {


        GetComponent<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);

    }
}
