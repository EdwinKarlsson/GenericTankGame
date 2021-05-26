using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTankController : MonoBehaviour
{
[Header("Settings")]
    [SerializeField] float mouseSensitivety = 2f;
    [SerializeField] float torqueMultiplier = 1f;

    [Header("Transforms")]
    [SerializeField] Transform roofCamera;
    [SerializeField] Transform frontCamera;
    [SerializeField] Transform hullT;
    [SerializeField] Transform turretT;
    [SerializeField] Transform cannonT;

    [Header("Wheels")]
    [SerializeField] List<WheelsInfo> wheelsInfos;
     float motorTorqueInput = 0;
    float brakePower = 0;

    float cameraPitch = 90f;
    float cannonPitch = 0f;

    bool forward = false;
    bool right = false;
    bool left = false;
    bool back = false;

    bool rightTrackForward = false;
    bool rightTrackBack = false;
    bool leftTrackForward = false;
    bool leftTrackBack = false;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        motorTorqueInput = Mathf.Abs(Input.GetAxis("Vertical") + Input.GetAxis("Horizontal")) * torqueMultiplier;
        brakePower = motorTorqueInput * 10;

        PlayerInput();

        UpdateMouseLook();

        PlayerInputTranslator();

        TrackInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = true;
        }
        else
        {
            right = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            left = true;
        }
        else
        {
            left = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            back = true;
        }
        else
        {
            back = false;
        }
    }

    private void TrackInput()
    {
        switch (leftTrackForward, leftTrackBack, rightTrackForward, rightTrackBack)
        {
            case (true, false, true, false): //Forward
                foreach (WheelsInfo wheelsInfo in wheelsInfos)
                {
                    if (wheelsInfo.leftSideWheel)
                    {
                        if (transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity).z < 0)
                        {
                            wheelsInfo.wheel.brakeTorque = brakePower;
                        }
                        else
                        {
                            wheelsInfo.wheel.brakeTorque = 0f;
                            wheelsInfo.wheel.motorTorque = motorTorqueInput;
                        }
                    }
                    if (!wheelsInfo.leftSideWheel)
                    {
                        if (transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity).z < 0)
                        {
                            wheelsInfo.wheel.brakeTorque = brakePower;
                        }
                        else
                        {
                            wheelsInfo.wheel.brakeTorque = 0f;
                            wheelsInfo.wheel.motorTorque = motorTorqueInput;
                        }
                    }
                }
                break;

            case (true, false, false, true): //Right
                foreach (WheelsInfo wheelsInfo in wheelsInfos)
                {
                    if (wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = 0f;
                        wheelsInfo.wheel.motorTorque = motorTorqueInput;
                    }
                    if (!wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = 0f;
                        wheelsInfo.wheel.motorTorque = -motorTorqueInput;
                    }
                }
                break;

            case (false, true, true, false): //Left
                foreach (WheelsInfo wheelsInfo in wheelsInfos)
                {
                    if (wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = 0f;
                        wheelsInfo.wheel.motorTorque = -motorTorqueInput;
                    }
                    if (!wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = 0f;
                        wheelsInfo.wheel.motorTorque = motorTorqueInput;
                    }
                }
                break;

            case (false, true, false, true): //Back
                foreach (WheelsInfo wheelsInfo in wheelsInfos)
                {
                    if (wheelsInfo.leftSideWheel)
                    {
                        if (transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity).z > 0)
                        {
                            wheelsInfo.wheel.brakeTorque = brakePower;
                        }
                        else
                        {
                            wheelsInfo.wheel.brakeTorque = 0f;
                            wheelsInfo.wheel.motorTorque = -motorTorqueInput;
                        }
                    }
                    if (!wheelsInfo.leftSideWheel)
                    {
                        if (transform.InverseTransformDirection(gameObject.GetComponent<Rigidbody>().velocity).z > 0)
                        {
                            wheelsInfo.wheel.brakeTorque = brakePower;
                        }
                        else 
                        {
                            wheelsInfo.wheel.brakeTorque = 0f;
                            wheelsInfo.wheel.motorTorque = -motorTorqueInput;
                        }
                    }
                }
                break;

            default:
                foreach (WheelsInfo wheelsInfo in wheelsInfos)
                {
                    if (wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = brakePower/2;
                        wheelsInfo.wheel.motorTorque = 0f;
                    }
                    if (!wheelsInfo.leftSideWheel)
                    {
                        wheelsInfo.wheel.brakeTorque = brakePower/2;
                        wheelsInfo.wheel.motorTorque = 0f;
                    }
                }
                break;
        }
    }

    private void PlayerInputTranslator()
    {
        switch (forward, right, left, back) //Player Input
        {
            case (true, false, false, false): //Forward
                rightTrackForward = true;
                rightTrackBack = false;
                leftTrackForward = true;
                leftTrackBack = false;
                break;

            case (true, true, false, false): //Forward Right
                rightTrackForward = false;
                rightTrackBack = false;
                leftTrackForward = true;
                leftTrackBack = false;
                break;

            case (true, false, true, false): //Forward left
                rightTrackForward = true;
                rightTrackBack = false;
                leftTrackForward = false;
                leftTrackBack = false;
                break;

            case (false, true, false, false): //Right
                rightTrackForward = false;
                rightTrackBack = true;
                leftTrackForward = true;
                leftTrackBack = false;
                break;

            case (false, false, true, false): //Left
                rightTrackForward = true;
                rightTrackBack = false;
                leftTrackForward = false;
                leftTrackBack = true;
                break;

            case (false, true, false, true): //Back Right
                rightTrackForward = false;
                rightTrackBack = false;
                leftTrackForward = false;
                leftTrackBack = true;
                break;

            case (false, false, true, true): //Back Left
                rightTrackForward = false;
                rightTrackBack = true;
                leftTrackForward = false;
                leftTrackBack = false;
                break;

            case (false, false, false, true): //Back
                rightTrackForward = false;
                rightTrackBack = true;
                leftTrackForward = false;
                leftTrackBack = true;
                break;

            default: //Nothing
                rightTrackForward = false;
                rightTrackBack = false;
                leftTrackForward = false;
                leftTrackBack = false;
                break;
        }
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        cameraPitch -= mouseDelta.y*mouseSensitivety;
        cameraPitch = Mathf.Clamp(cameraPitch, 75f, 100f);

            cannonPitch -= mouseDelta.y * mouseSensitivety;
            cannonPitch = Mathf.Clamp(cannonPitch, -15f, 10f);
            cannonT.localEulerAngles = Vector3.right * (cannonPitch+90);

        roofCamera.localEulerAngles = Vector3.right * cameraPitch;
        frontCamera.localEulerAngles = Vector3.right * cameraPitch;

        turretT.Rotate(Vector3.forward * mouseDelta.x * mouseSensitivety);
    }

    //https://docs.unity3d.com/Manual/WheelColliderTutorial.html To make tank move
    [System.Serializable]
    public class WheelsInfo
    {
        public WheelCollider wheel;
        public bool leftSideWheel;
    }
}
