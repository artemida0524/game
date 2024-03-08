using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TakeGun : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10.0f;
    private Ray ray;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject targetForWeapon;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    public GameObject currentWeapon;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward * maxDistance);
        
        if (Input.GetKeyDown(KeyCode.R))
        {

            Drop();

        }



        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance))
        {

            if (hitInfo.collider.tag == "gun" && Input.GetKeyDown(KeyCode.F))
            {
                if (currentWeapon != null)
                {
                    Drop();
                }

                Take(hitInfo.collider.gameObject);
            }
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (currentWeapon == null && collision.gameObject.tag == "gun")
        {
            Take(collision.gameObject);
        }
    }

    private void Take(GameObject hitInfo)
    {
        GetComponent<Shooting>().currentWeapon = hitInfo;
        currentWeapon = hitInfo;
        currentWeapon.GetComponent<Animator>().enabled = true;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;

        currentWeapon.transform.position = targetForWeapon.transform.position;
        currentWeapon.transform.rotation = targetForWeapon. transform.rotation;
        currentWeapon.transform.parent = targetForWeapon.transform;

        currentWeapon.GetComponent<Gun>().isTake = false;

    }

    private void Drop()
    {
        try
        {
            GetComponent<Shooting>().currentWeapon = null;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            currentWeapon.GetComponent<Animator>().enabled = false;
            currentWeapon.GetComponent<Rigidbody>().AddForce(transform.forward * 150f);
            currentWeapon.transform.parent = null;
            currentWeapon = null;
        }
        catch (Exception)
        {
            textMeshProUGUI.text = "Weapon Empty";
            StartCoroutine(GetComponent<Shooting>().Wait());
        }
    }
}
