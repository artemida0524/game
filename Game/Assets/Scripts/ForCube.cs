using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ForCube : MonoBehaviour
{
    private bool isWork = true;
    private float speedForJump = 5.0f;
    [SerializeField] float speed = 0.0f;
    [SerializeField] GameObject objPrefab1;
    [SerializeField] GameObject objPrefab2;
    [SerializeField] GameObject objPrefab3;
    [SerializeField] ParticleSystem objParticleSystem;
    //private Thread spawner;

    void Start()
    {
        StartCoroutine(CoroutineSpawn());


    }


    void Update()
    {

        if (isWork)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            float x = Input.GetAxis("Vertical");
            float z = Input.GetAxis("Horizontal");

            transform.position += new Vector3(x, 0, -z) * speed * Time.deltaTime;
        }
        
        

    }

    private IEnumerator CoroutineSpawn()
    {
        while (true) { Spawn(); yield return new WaitForSeconds(1.5f); }


    }
    private void Spawn()
    {

        float random = UnityEngine.Random.Range(1, 4);
        
        if (random == 1)
        {
            Instantiate(objPrefab1, new Vector3(150.0f, 6.0f, -0.73f), Quaternion.identity);
        }

        if (random == 2)
        {
            Instantiate(objPrefab2, new Vector3(150.0f, 6.0f, 0.45f), Quaternion.identity);
        }

        if (random == 3)
        {
            Instantiate(objPrefab3, new Vector3(150.0f, 6.0f, 0.26f), Quaternion.identity);
        }
    }

    private void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * speedForJump, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "destroy") {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshRenderer>().enabled = false;
            objParticleSystem.Play();
            isWork = false;
        }
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
    
}