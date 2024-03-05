using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nigger : MonoBehaviour
{
    Animator animator;
    [SerializeField] private float speed = 0f;
    private bool walk = false;
    private bool walkLeft = false;
    private bool walkRight = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("walk", walk);
        animator.SetBool("walkLeft", walkLeft);
        animator.SetBool("WalkRight", walkRight);

        if (Input.GetKey(KeyCode.W)) { 
            transform.Translate(new Vector3(0,0,0.5f) * speed * Time.deltaTime);
            walk = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            walkLeft = false;
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime);
            walk = false;
        }

        if(Input.GetKey(KeyCode.A)) { 
            walkLeft = true;
            transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            walkLeft = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            walkRight = true;
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            walkRight = false;
        }


    }
}
