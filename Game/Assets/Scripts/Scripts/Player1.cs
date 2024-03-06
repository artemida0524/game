using Cinemachine;

using UnityEngine;

using UnityEngine.SceneManagement;


public class Player1 : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject shark;
    [SerializeField] private GameObject bodySkeleton;
    [SerializeField] private CinemachineVirtualCamera cameraFace1;
    [SerializeField] private CinemachineVirtualCamera cameraFace3;


    private Animator animatorShark;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedForWalk = 1.0f;
    [SerializeField] private float speedForRotate = 3f;
    [SerializeField] private float speedForLeftRight = 0;


    private float smoothTime = 0.2f;
    private float horizontal;
    private float vertical;
    private float smoothVelocity_ForUpHorizontal = 0.0f;
    private float smoothVelocity_ForDownHorizontal = 0.0f;
    private float smoothVelocity_ForDownVertical = 0.0f;
    private float smoothVelocity_ForUpVertical = 0.0f;

    private bool isW = true;
    private bool isRun = true;
    private bool forDownVerticalBool;
    private bool forUpVerticalBool;
    private bool forDownHorizontalBool;
    private bool forUpHorizontalBool;
    private bool forTurnLeft = true;
    private bool forTurnRight = true;
    private bool isGround = true;



    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animatorShark = shark.GetComponent<Animator>();
    }


    void Update()
    {
        Debug.Log(cameraFace1.enabled + " " + cameraFace3.enabled);
        if (Input.GetKeyDown(KeyCode.T))
        {
            SwitchCamera();
        }
        animator.SetBool("isGround", isGround);
        //animator.SetBool("getHit", getHit);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ControllerInGround();
        //Boxing();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();

        }
    }
    private void SwitchCamera()
    {
        if (cameraFace1.Priority == 11)
        {
            bodySkeleton.SetActive(true);
            cameraFace1.Priority = 9;
        }
        else if (cameraFace1.Priority == 9)
        {
            bodySkeleton.SetActive(false);
            cameraFace1.Priority = 11;
        }

    }
    private void Boxing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Boxing");
        }
    }

    private void ControllerInWater()
    {
        if (forDownVerticalBool)
        {
            ForDownVertical();
        }
        if (forDownHorizontalBool)
        {
            ForDownHorizontal();
        }
        if (forUpHorizontalBool)
        {
            ForUpHorizontal();
        }

        if (forUpVerticalBool)
        {
            ForUpVertical();
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (gameObject.transform.position.y <= -14.4f)
        {
            gameObject.transform.position = new Vector3(160.987f, 9.925f, 396.836f);
        }




    }
    private void ControllerInGround()
    {
        if (forDownVerticalBool)
        {
            ForDownVertical();
        }
        if (forDownHorizontalBool)
        {
            ForDownHorizontal();
        }
        if (forUpHorizontalBool)
        {
            ForUpHorizontal();
        }

        if (forUpVerticalBool)
        {
            ForUpVertical();
        }

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (gameObject.transform.position.y <= -14.4f)
        {
            gameObject.transform.position = new Vector3(160.987f, 9.925f, 396.836f);
        }

        if (Input.GetMouseButtonDown(0))
        {

            animator.SetTrigger("Boxing");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            vertical -= 0.01f;
            isRun = false;
            isW = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertical -= Time.deltaTime;
            vertical = Mathf.Clamp(vertical, -1.0f, 0.0f);
            transform.Translate(new Vector3(0, 0, vertical) * speedForLeftRight * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            forUpVerticalBool = true;
            isW = true;
            isRun = true;
        }


        if (Input.GetKey(KeyCode.A) && forTurnLeft)
        {
            horizontal -= 0.01f;
            isRun = false;
            forTurnRight = false;
        }

        if (Input.GetKey(KeyCode.A) && forTurnLeft)
        {
            horizontal -= Time.deltaTime;
            horizontal = Mathf.Clamp(horizontal, -1, 0);
            transform.Translate(new Vector3(horizontal, 0, 0) * speedForLeftRight * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.A) && forTurnLeft)
        {
            forDownHorizontalBool = true;
            isRun = true;
        }

        if (Input.GetKey(KeyCode.W) && isW)
        {
            forDownVerticalBool = false;
            vertical += Time.deltaTime;
            vertical = Mathf.Clamp(vertical, 0, 0.4f);
            transform.Translate(new Vector3(0, 0, vertical) * speedForWalk * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            forDownVerticalBool = true;
        }

        if (Input.GetKey(KeyCode.D) && forTurnRight)
        {
            horizontal += 0.01f;
            isRun = false;
            forTurnLeft = false;
        }
        if (Input.GetKey(KeyCode.D) && forTurnRight)
        {
            //forTurnLeft = false;
            transform.Translate(new Vector3(horizontal, 0, 0) * speedForLeftRight * Time.deltaTime);

            horizontal += Time.deltaTime;
            horizontal = Mathf.Clamp(horizontal, 0.0f, 1.0f);
        }

        if (Input.GetKeyUp(KeyCode.D) && forTurnRight)
        {
            isRun = true;
            forUpHorizontalBool = true;
        }

        if (Input.GetKey(KeyCode.W) && isW)
        {
            forDownVerticalBool = false;
            vertical += Time.deltaTime;
            vertical = Mathf.Clamp(vertical, 0, 0.4f);
            transform.Translate(new Vector3(0, 0, vertical) * speedForWalk * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {

            forDownVerticalBool = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isRun)
        {


            forTurnLeft = false;
            forTurnRight = false;
            forDownVerticalBool = false;
            isW = false;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && isRun)
        {
            forTurnLeft = false;
            forTurnRight = false;
            isW = false;
            forDownVerticalBool = false;
            vertical += Time.deltaTime;
            vertical = Mathf.Clamp(vertical, 0.0f, 1.0f);
            transform.Translate(new Vector3(0, 0, vertical) * speed * Time.deltaTime);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            forTurnLeft = true;
            forTurnRight = true;
            forDownVerticalBool = true;

        }
    }

    private void ForUpHorizontal()
    {


        if (horizontal < 0.3f)
        {
            forTurnLeft = true;
        }
        if (horizontal < 0.08f)
        {
            horizontal = 0.0f;
            forUpHorizontalBool = false;
            return;
        }
        horizontal = Mathf.SmoothDamp(horizontal, 0.0f, ref smoothVelocity_ForUpHorizontal, smoothTime);
    }

    public void ForDownHorizontal()
    {
        if (horizontal > -0.3f)
        {
            forTurnRight = true;
        }
        if (horizontal > -0.08f)
        {
            horizontal = 0.0f;
            forDownHorizontalBool = false;
            return;
        }
        horizontal = Mathf.SmoothDamp(horizontal, 0.0f, ref smoothVelocity_ForDownHorizontal, smoothTime);
    }

    public void ForDownVertical()
    {
        if (vertical < 0.4f)
        {
            isW = true;
        }
        if (vertical < 0.005f)
        {
            vertical = 0.0f;
            forDownVerticalBool = false;

            return;
        }

        vertical = Mathf.SmoothDamp(vertical, 0.0f, ref smoothVelocity_ForDownVertical, smoothTime);
    }

    public void ForUpVertical()
    {
        if (vertical > -0.4f)
        {
            isW = true;
        }
        if (vertical > -0.005f)
        {
            vertical = 0.0f;
            forUpVerticalBool = false;

            return;
        }

        vertical = Mathf.SmoothDamp(vertical, 0.0f, ref smoothVelocity_ForUpVertical, smoothTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "middleFinger")
        {
            animator.SetTrigger("GetHit");
        }
        if (other.gameObject.name == "Waters")
        {
            isGround = false;
        }
        if (other.gameObject.name == "WaterForShark")
        {
            animatorShark.SetBool("Attack", true);
        }

        /*Tp scene*/
        {
            if (other.gameObject.name == "TpPressToMainScene")
            {
                SceneManager.LoadScene("Terrain");
            }
            if (other.gameObject.name == "TpMainSceneToPress")
            {
                SceneManager.LoadScene("Press");
            }
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Waters")
        {
            isGround = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Waters")
        {
            
            isGround = true;
        }
        if (other.gameObject.name == "WaterForShark")
        {
            animatorShark.SetBool("Attack", false);
        }
    }
    private void Jump()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.1f))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 4, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }
}