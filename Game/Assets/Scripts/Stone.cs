using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{



    [SerializeField] private GameObject stone;
    private Animator animator;

    public float hp;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Bam()
    {
        Debug.Log("Bam");
        hp -= 3;
        animator.SetTrigger("shake");
        if (hp <= 0)
        {
            hp = 20;
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(stone, transform.position, Quaternion.identity);
                obj.transform.position += new Vector3(Random.Range(-1.0f, 1.0f), 1.0f, Random.Range(-1.0f,1.0f));
            }
            Instantiate(transform.parent, new Vector3(Random.Range(0.0f, 439.0f), 50.0f, Random.Range(56.0f, 447.0f)), Quaternion.identity);

            Destroy(gameObject);
        }
    }
}