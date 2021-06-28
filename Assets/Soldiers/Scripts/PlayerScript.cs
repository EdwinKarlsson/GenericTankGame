using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Settings")] //Things that will either be controlled by the player or needs further tweaking
    [SerializeField] float mouseSensitivity;
    [SerializeField] float movementSpeed;
    [SerializeField] float fallingMovementSpeed;
    [SerializeField] float groundDistance = 0.4f;
    public float gravityMultiplier = 1;

    [Header("Other")]
    //Camera
    [SerializeField] Transform myCamera;
    float cameraPitch;
    //Movement
    Vector3 velocity;
    CharacterController myCC;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        myCC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        PlayerMovement();
        MouseLook();
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        myCC.Move(move*movementSpeed*Time.deltaTime);

        velocity.y += Physics.gravity.y*gravityMultiplier*Time.deltaTime;
        myCC.Move(velocity * Time.deltaTime);

        Debug.Log(velocity.y);
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
