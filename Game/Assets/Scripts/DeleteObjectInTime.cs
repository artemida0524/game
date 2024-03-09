using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObjectInTime : MonoBehaviour
{
    [SerializeField] float time;
    private void Start()
    {
        StartCoroutine(Delete(time));
    }

    private IEnumerator Delete(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}