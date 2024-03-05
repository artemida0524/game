using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using System;
using TMPro;

public class Shooting : MonoBehaviour
{
    public GameObject currentWeapon;
    Animator animator;
    [SerializeField] public TextMeshProUGUI text;
    private bool shoot = true;

    



    

    

    private void Update()
    {

        if (currentWeapon != null)
        {
            if (currentWeapon.GetComponent<Gun>().typeOfGun == Gun.TypeOfGun.rapidFire)
            {
                if (Input.GetMouseButton(0))
                {
                    ShootRapid();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    animator.SetBool("Shoot", false);
                }
            }

            if (currentWeapon.GetComponent<Gun>().typeOfGun == Gun.TypeOfGun.single)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    animator.SetBool("Shoot", false);
                }
            }

        }



    }
    private IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(0.1f);
        shoot = true;
    }
    public void ShootRapid()
    {
        try
        {
            Debug.Log(currentWeapon.name);
        }
        catch
        {
            text.text = "Weapon Empty";
            StartCoroutine(Wait());
        }

        if (currentWeapon != null)
        {
            if (shoot)
            {
                StartCoroutine(StartShoot());
                shoot = false;
                animator = currentWeapon.GetComponent<Animator>();
                animator.SetBool("Shoot", true);
                currentWeapon.GetComponent<Gun>().Shoot();
            }
            
        }

    }

    public void Shoot()
    {
        try
        {
            //currentWeapon = GetComponent<TakeGun>().currentWeapon;
            Debug.Log(currentWeapon.name);
        }
        catch (Exception)
        {
            text.text = "Weapon Empty";
            StartCoroutine(Wait());
        }

        if (currentWeapon != null)
        {
            animator = currentWeapon.GetComponent<Animator>();
            animator.SetBool("Shoot", true);
            currentWeapon.GetComponent<Gun>().Shoot();

        }


    }

    public IEnumerator Wait()
    {

        yield return new WaitForSeconds(2);
        text.text = string.Empty;

    }





}
