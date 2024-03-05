using UnityEngine;


public class FPSController : MonoBehaviour
{
    [SerializeField] public Camera camera;
    [SerializeField] GameObject target;
    private Rigidbody rigidbody;

    float speedForWASD = 1000f;
    float speedForRotate = 500f;
    float slideMouseX;
    float slideMouseY;
    float jump = 8f;
    private Vector3 sitDown = new Vector3(1, 0.8f, 1);
    private Vector3 standUp = new Vector3(1, 1, 1);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {


        //if(gameObject.transform.position.y <= 386.58f)
        //{
        //    gameObject.transform.position = new Vector3(532.84f, 389.85f, 1028.44f);
        //}

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            transform.localScale = sitDown;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = standUp;
        }

        slideMouseX = Input.GetAxis("Mouse X");
        slideMouseY = Input.GetAxis("Mouse Y");



        target.transform.Rotate(new Vector3(-slideMouseY, 0, 0) * speedForRotate * Time.deltaTime);

        transform.Rotate(new Vector3(0, slideMouseX, 0) * speedForRotate * Time.deltaTime);


        rigidbody.angularVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            Shift();
            rigidbody.AddForce(transform.forward * speedForWASD * Time.deltaTime);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60, 0.1f);
            speedForWASD = 1000;
        }
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60, 0.1f);
            speedForWASD = 1000;
        }

        if (Input.GetKey(KeyCode.A))
        {

            rigidbody.AddForce(-transform.right * speedForWASD * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {

            rigidbody.AddForce(-transform.forward * speedForWASD * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.AddForce(transform.right * speedForWASD * Time.deltaTime);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                rigidbody.AddForce(Vector3.up * jump, ForceMode.Impulse);
            }
        }

    }

    private void Shift()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 75, 0.1f);
            speedForWASD = 1400;
        }
        else
        {
            camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, 60, 0.1f);
            speedForWASD = 1000;
        }
    }
}
