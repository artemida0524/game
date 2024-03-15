using Cinemachine.Utility;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class Tree : MonoBehaviour
{

    [SerializeField] private float time;
    [SerializeField] private GameObject wood;
    [SerializeField] private GameObject tree;
    private Animator animator;

    public float hp;

    private float timeUp = 0;
    [SerializeField] GameObject obj;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeUp += Time.deltaTime;
        if (timeUp >= time)
        {
            Vector3 vector3 = transform.position;
            vector3.x += Random.Range(-4.0f, 4.0f);
            vector3.y += Random.Range(3f, 6.0f);
            vector3.z += Random.Range(-4.0f, 4.0f);
            timeUp = 0;
            Instantiate(obj, vector3, Quaternion.identity);
        }
    }
    public void Chop()
    {
        hp -= 3;
        animator.SetTrigger("Chop");
        
    }
    public void DeleteObject()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(wood, transform.position, Quaternion.identity);
            obj.transform.position += new Vector3(Random.Range(-1.0f, 1.0f), 1.0f, Random.Range(-1.0f, 1.0f));
        }


        Instantiate(transform.parent, new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f)), Quaternion.identity);

        Destroy(tree);

    }
}