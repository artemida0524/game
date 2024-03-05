using System.Text.RegularExpressions;
using Unity.Android.Types;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speedForToHead = 10.0f;
    private float xRotate;
    private float yRotate;
    [SerializeField] private float rotateSens;
    [SerializeField] GameObject targetObj;
    [SerializeField] GameObject headObj;
    [SerializeField] private GameObject capsule;

    void Update()
    {
        transform.position = /*headObj.transform.position;*/ Vector3.Lerp(transform.position, headObj.transform.position, Time.deltaTime * speedForToHead);
        MouseRotate();

    }

    private void MouseRotate()
    {

        xRotate += Input.GetAxis("Mouse X");
        yRotate += Input.GetAxis("Mouse Y") * rotateSens;
        yRotate = Mathf.Clamp(yRotate, -90f, 90f);
        targetObj.transform.rotation = Quaternion.Euler(0f, xRotate, 0f);
        if (capsule != null)
        {
            capsule.transform.rotation = Quaternion.Euler(0f, xRotate * 2, 0f);
        }
        transform.rotation = Quaternion.Euler(-yRotate, xRotate, 0f);
    }
}
