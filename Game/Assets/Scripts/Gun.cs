using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private GameObject targetBullet;
    [SerializeField] private GameObject sourceBullet;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] public TypeOfGun typeOfGun;
    public bool isTake = true;

    public enum TypeOfGun
    {
        rapidFire,
        single
    }
    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(sourceBullet, targetBullet.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(-transform.forward * 1000);
        boom.Play();
        StartCoroutine(Stop());

    }

    private IEnumerator Stop()
    {

        yield return new WaitForSeconds(0.5f);
        boom.Stop();
    }

    private void Update()
    {
        if(isTake)
        {
            transform.Rotate(new Vector3(0,1,0) * Time.deltaTime * 100);
        }
    }

}
