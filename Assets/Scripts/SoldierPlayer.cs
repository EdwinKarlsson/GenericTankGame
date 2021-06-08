using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float mouseSensitivity;
    //Movement
    [SerializeField] float movementSpeed;
    [SerializeField] float maximumMovementSpeed;
    [SerializeField] float fallingMovementSpeed;

    [Header("Transforms")]
    [SerializeField] Transform myCamera;

    [Header("Colliders")]
    [SerializeField] Collider feetCollider;
    [SerializeField] PhysicMaterial[] feetSticky;


    //Misc

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
        Debug.Log(myRB.velocity.magnitude);
        if (forward)
        {
            myRB.AddForce(transform.forward * movementSpeed, ForceMode.VelocityChange);
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
