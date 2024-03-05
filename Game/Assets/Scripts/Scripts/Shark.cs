using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shark : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject mainOblect;
    
    float time;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, 5.39f, transform.position.z);
        Vector3 direction = mainOblect.transform.position - transform.position;
        if (animator.GetBool("Attack"))
        {
            Attack(direction);
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        transform.Translate(new Vector3(0, 0, 2) * Time.deltaTime);
        time += Time.deltaTime;
        if (time >= 20)
        {
            transform.Rotate(new Vector3(0, UnityEngine.Random.Range(-180, 180), 0));
            time = 0;
        }

    }

    private void Attack(Vector3 direction)
    {
        float distance = direction.magnitude;
        transform.rotation = Quaternion.LookRotation(direction);

        if (distance >= 3)
        {
            transform.Translate(new Vector3(0, 0, 3) * Time.deltaTime);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z));
        }
    }

}
