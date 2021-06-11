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

    bool falling = false;
    bool toFast = false;

    Rigidbody myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        MouseLook();
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        GetComponent<CharacterController>().Move(move*movementSpeed*Time.deltaTime);
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
