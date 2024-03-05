using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Dark : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] public Color color;
    private float speed = 1.2f;

    public bool isEclipse = false;
    
    private void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }
    private void Update()
    {
        Eclipse();
        
    }

    private void Eclipse()
    {
        if (color.a < 1 && isEclipse)
        {
            color.a += Time.deltaTime * speed;
            image.color = color;
        }
        if (color.a >= 1 && isEclipse)
        {
            
            isEclipse = false;
        }
    }
    

    public void Timeline4()
    {
        if (color.a >= 0)
        {
            color.a -= Time.deltaTime * speed;
            image.color = color;
        }

    }
}