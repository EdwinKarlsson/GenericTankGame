using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float mouseSensitivity;
    //Movement
    [SerializeField] float movementSpeed;
    [SerializeField] float fallingMovementSpeed;

    [Header("Transforms")]
    [SerializeField] Transform myCamera;

    float cameraPitch;

    bool forward = false;
    bool right = false;
    bool back = false;
    bool left = false;
    bool falling = false;

    Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputTranslator();
        PlayerMovement();
        MouseLook();
    }

    private void PlayerMovement()
    {
        switch (forward, right, back, left, falling)
        {
            case (true, false, false, false, false):
                myRB.AddForce(transform.forward * movementSpeed);
                break;

            case (true, false, false, false, true):
                myRB.AddForce(transform.forward * fallingMovementSpeed);
                break;

            case (true, true, false, false, false):
                myRB.AddForce((transform.forward + transform.right) * movementSpeed / 2);
                break;

            case (true, true, false, false, true):
                myRB.AddForce((transform.forward + transform.right) * fallingMovementSpeed / 2);
                break;

            case (false, true, false, false, false):
                myRB.AddForce(transform.right * movementSpeed);
                break;

            case (false, true, false, false, true):
                myRB.AddForce(transform.right * fallingMovementSpeed);
                break;

            case (false, true, true, false, false):
                myRB.AddForce((transform.right - transform.forward) * movementSpeed / 2);
                break;

            case (false, true, true, false, true):
                myRB.AddForce((transform.right - transform.forward) * fallingMovementSpeed / 2);
                break;

            case (false, false, true, false, false):
                myRB.AddForce((-transform.forward) * movementSpeed);
                break;

            case (false, false, true, false, true):
                myRB.AddForce((-transform.forward) * fallingMovementSpeed);
                break;

            case (false, false, true, true, false):
                myRB.AddForce((-transform.right - transform.forward) * movementSpeed / 2);
                break;

            case (false, false, true, true, true):
                myRB.AddForce((-transform.right - transform.forward) * fallingMovementSpeed / 2);
                break;

            case (false, false, false, true, false):
                myRB.AddForce((-transform.right) * movementSpeed);
                break;

            case (false, false, false, true, true):
                myRB.AddForce((-transform.right) * fallingMovementSpeed);
                break;

            case (true, false, false, true, false):
                myRB.AddForce((-transform.right + transform.forward) * movementSpeed / 2);
                break;

            case (true, false, false, true, true):
                myRB.AddForce((-transform.right + transform.forward) * fallingMovementSpeed / 2);
                break;

            default:
                if (myRB.velocity.magnitude < 0.1)
                {
                    myRB.velocity = Vector3.zero;
                }
                break;
        }
    }

    private void PlayerInputTranslator()
    {
        forward = Input.GetKey(KeyCode.W);
        right = Input.GetKey(KeyCode.D);
        back = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
    }

    private void MouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90f);

        myCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }
}
