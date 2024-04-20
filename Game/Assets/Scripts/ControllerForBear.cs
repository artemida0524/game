using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using UnityEngine;

public class ControllerForBear : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] private GameObject hand1;
    [SerializeField] private GameObject hand2;

    private float timeWalkSeconds = 0f;
    private float timeIdle = 0f;
    private float randomWalkSeconds;
    private float randomIdle;
    float waitAnimation = 0f;

    private bool idle = true;
    private bool standart = true;
    private bool attackNow = false;
    private bool attack = false;
    private bool isActivated = false;



    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("standart", standart);
        randomIdle = UnityEngine.Random.Range(5f, 10f);
        randomWalkSeconds = UnityEngine.Random.Range(3f, 6f);
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -14.4f)
        {
            gameObject.transform.position = new Vector3(160.987f, 9.925f, 396.836f);
        }
        Vector3 direction = Player1.Instance.transform.position - transform.position;
        float distance = direction.magnitude;
        
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (distance <= 10f && !attackNow)
        {
            attackNow = true;
        }

        if(attackNow)
        {
            Attack(direction);
        }

        if (standart)
        {
            Standart();
        }

    }
    private void Attack(Vector3 direction)
    {
        float distance = direction.magnitude;

        if (standart)
        {
            standart = false;
        }

        waitAnimation += Time.deltaTime;
        animator.SetBool("standart", standart);
        transform.rotation = Quaternion.LookRotation(direction);
        if (distance <= 3)
        {
            
            attack = true;
            animator.SetBool("attack", attack);
        }
        else
        {
            attack = false;
            animator.SetBool("attack", attack);
        }
        if (waitAnimation >= 1.6f && distance >= 2.7f)
        {
            transform.Translate(new Vector3(0, 0, 3) * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(direction);
            
        }
    }

    private void Standart()
    {

        if (randomWalkSeconds >= timeWalkSeconds && !idle)
        {
            timeWalkSeconds += Time.deltaTime;
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
            if (randomWalkSeconds <= timeWalkSeconds)
            {
                timeWalkSeconds = 0f;
                idle = true;
                animator.SetBool("walk", !idle);
            }
        }

        if (idle)
        {
            timeIdle += Time.deltaTime;

            if (randomIdle <= timeIdle)
            {

                idle = false;
                timeIdle = 0f;

                transform.Rotate(new Vector3(0, UnityEngine.Random.Range(-360, 360), 0));
                animator.SetBool("walk", !idle);
            }
        }
    }

    
}