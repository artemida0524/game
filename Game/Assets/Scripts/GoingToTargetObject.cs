using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GoingToTargetObject : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GlobalCameraInteractible globalCameraInteractible;

    public Transform target;
    [NonSerialized] public float timeToReachTarget = 3f;

    private Vector3 initialPosition;
    private float elapsedTime = 0f;

    private bool isSetPosition;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = globalCameraInteractible.currentComponentSceneItem.pointer.transform.position;

        initialPosition = transform.position;
    }

    private void Update()
    {

        if (target != null)
        {

            if (isSetPosition)
            {
                initialPosition = transform.position;
                isSetPosition = false;
            }


            if (elapsedTime < timeToReachTarget)
            {

                float t = elapsedTime / timeToReachTarget;

                transform.position = Vector3.Lerp(initialPosition, target.position, t);

                elapsedTime += Time.deltaTime;

                if (elapsedTime > timeToReachTarget)
                {
                    target = null;
                    elapsedTime = 0f;
                    isSetPosition = true;
                }

            }
        }
    }
}