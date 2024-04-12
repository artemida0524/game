using Cinemachine;
using System;
using UnityEditor.PackageManager;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

using UnityEngine.SceneManagement;


public class Player1 : MonoBehaviour
{

    public static Player1 Instance { get; private set; }

    private const int smoothFieldOfView = 40;
    [SerializeField] private Animator animator;
    [SerializeField] private InteractibleIndecator interactibleIndecator;
    [SerializeField] private GameObject inventroy;
    [SerializeField] private InventoryData inventoryData;
    [SerializeField] private GameObject shark;
    [SerializeField] private GameObject bodySkeleton;
    [SerializeField] private CinemachineVirtualCamera cinemachineCameraFace1;
    [SerializeField] private Camera cameraFrom3;
    [SerializeField] private Camera cameraFrom1;
    [SerializeField] private Camera miniMapCamera;

    [SerializeField] GameObject objBuild;
    bool isBuild = false;


    private TakeObject takeObject;
    private Animator animatorShark;
    public Camera currentCamera;

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
    private bool isBoxing = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        inventoryData = GetComponent<InventoryData>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animatorShark = shark.GetComponent<Animator>();
        currentCamera = cameraFrom1;
        takeObject = GetComponent<TakeObject>();
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            isBoxing = !isBoxing;
        }

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

        Ray ray = new Ray(currentCamera.transform.position, currentCamera.transform.forward);

        //if (Input.GetKeyDown(KeyCode.I) && !booltest)
        //{
        //    booltest = true;
        //    objTest = Instantiate(objTest, new Vector3(0, 0, 0), objTest.transform.rotation);
        //    objTest.GetComponent<BoxCollider>().enabled = false;
        //    takeObject.isTake = false;
        //}


        if (Physics.Raycast(ray, out RaycastHit hitInfo, takeObject.maxDistance) && isBuild)
        {
            objBuild.transform.position = hitInfo.point;
            objBuild.transform.position += new Vector3(0, 0.4f, 0);

            if (Input.GetKey(KeyCode.Z))
            {
                objBuild.transform.Rotate(new Vector3(0, 0, -40) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.X))
            {
                objBuild.transform.Rotate(new Vector3(0, 0, 40) * Time.deltaTime);
            }
            if (Input.GetMouseButtonDown(0))
            {
                isBuild = false;
                objBuild.GetComponent<BoxCollider>().enabled = true;
                takeObject.isTake = true;
                inventoryData.RemoveData(objBuild.GetComponent<ObjectData>().id, objBuild.GetComponent<ObjectData>().count);
            }
        }

    }


    public void Build(GameObject objBuild)
    {
        isBuild = true;
        objBuild = Instantiate(objBuild, new Vector3(0, 0, 0), objBuild.transform.rotation);
        objBuild.GetComponent<BoxCollider>().enabled = false;
        takeObject.isTake = false;
        inventroy.SetActive(false);
        this.objBuild = objBuild;
        
    }

    private void SwitchCamera()
    {

        if (cinemachineCameraFace1.Priority == 11)
        {
            bodySkeleton.SetActive(true);
            cinemachineCameraFace1.Priority = 9;
            cameraFrom3.gameObject.SetActive(true);
            cameraFrom1.gameObject.SetActive(false);

            currentCamera = cameraFrom3;

            takeObject.maxDistance = 5f;
        }

        else if (cinemachineCameraFace1.Priority == 9)
        {

            bodySkeleton.SetActive(false);
            cinemachineCameraFace1.Priority = 11;
            cameraFrom3.gameObject.SetActive(false);
            cameraFrom1.gameObject.SetActive(true);
            currentCamera = cameraFrom1;
            takeObject.maxDistance = 2f;
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

        if (Input.GetMouseButtonDown(0) && isBoxing)
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

            miniMapCamera.fieldOfView += Time.deltaTime * smoothFieldOfView;
            miniMapCamera.fieldOfView = Mathf.Clamp(miniMapCamera.fieldOfView, 60, 70);

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

            miniMapCamera.fieldOfView += Time.deltaTime * smoothFieldOfView;
            miniMapCamera.fieldOfView = Mathf.Clamp(miniMapCamera.fieldOfView, 60, 80);
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


        miniMapCamera.fieldOfView -= Time.deltaTime * smoothFieldOfView;
        miniMapCamera.fieldOfView = Mathf.Clamp(miniMapCamera.fieldOfView, 60, 80);

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
        if (other.gameObject.layer == 12)
        {
            animator.SetTrigger("GetHit");
            interactibleIndecator.itemsIndecator[0].count.text = $"{int.Parse(interactibleIndecator.itemsIndecator[0].count.text) - 3} ";


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