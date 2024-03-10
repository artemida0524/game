using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    [SerializeField] GameObject target;


    private void Update()
    {
        transform.rotation = target.transform.rotation;
    }
}
