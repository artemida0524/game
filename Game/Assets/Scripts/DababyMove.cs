using Unity.VisualScripting;
using UnityEngine;


public class DababyMove : MonoBehaviour
{
    public Transform targetObject; 
    public float moveSpeed = 5.0f; 
    private float speedForThrow;
    void Update()
    {
        speedForThrow = Time.deltaTime;
        if (targetObject != null)
        {
            Vector3 direction = new Vector3(targetObject.position.x - transform.position.x, targetObject.position.y - transform.position.y, targetObject.position.z - transform.position.z);
            //Vector3 direction = targetObject.position - transform.position;

            direction.Normalize();

            transform.Translate(new Vector3(direction.x, direction.y, direction.z )* moveSpeed * Time.deltaTime);
        }
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "MobileMaleFreeSimpleMovement1 (1)")
        {
            targetObject.position += new Vector3(30, 0, 0) ;

        }
    }

}
