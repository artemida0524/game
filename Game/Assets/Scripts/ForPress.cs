using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ForPress : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isUp = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) {

            animator.SetBool("isUp", isUp);
            isUp = !isUp;   

        }
    }

    
}
