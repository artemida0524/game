using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artilerry : MonoBehaviour
{
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject targetObject;
    private bool shoot = true;
    private float mag;

    private void Update()
    {
        Vector3 result = transform.position - mainObject.transform.position;
        mag = result.magnitude;
        Debug.Log(mag);

        if(mag <= 100)
        {
            transform.rotation = Quaternion.LookRotation(result);
        }


        if (mag <= 100 && shoot)
        {
            if (shoot)
            {
                StartCoroutine(StartShoot());
            }
            shoot = false;
            
            GameObject obj = Instantiate(bullet, targetObject.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(-transform.forward * 500);

            
        }

    }
    private IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(0.2f);
        shoot = true;
    }
}
