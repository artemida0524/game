using System.Collections;

using UnityEngine;


public class Bullet : MonoBehaviour
{
    private ParticleSystem deleteParticle;
    private void Start()
    {
        
        StartCoroutine(Delete(3));
    }
    private void OnCollisionEnter(Collision collision)
    {
        //deleteParticle.Play();
        GetComponent<Renderer>().enabled = false;
        //GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(Delete(0.1f));

    }
    private IEnumerator Delete(float a)
    {
        
        yield return new WaitForSeconds(a);
        Destroy(gameObject);
    }

}