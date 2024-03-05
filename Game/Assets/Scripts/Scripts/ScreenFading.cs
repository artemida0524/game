using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Остальной код вашего скрипта


public class BlinkingObject : MonoBehaviour
{
    private float speed = 2.0f;
    IEnumerator Start()
    {
        Debug.Log("nigger");
        var image = GetComponent<Image>();
        Color color = image.color;

        while (color.a < 1f)
        {
            color.a += speed * Time.deltaTime;
            image.color = color;
            yield return null;

        }
    }
}