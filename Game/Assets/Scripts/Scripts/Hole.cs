using UnityEngine;

public class Hole : MonoBehaviour
{

    [SerializeField] GameObject Clown;
    [SerializeField] GameObject Morty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Clown")
        {
            Clown.SetActive(false);

        }
        if(other.gameObject.name == "Heel Morty (Morty)")
        {
            Morty.SetActive(false);
        }
    }
}
