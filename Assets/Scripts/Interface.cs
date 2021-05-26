using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    [SerializeField]Camera frontCam = null;
    [SerializeField] Camera roofCam = null;
    void Start()
    {
        frontCam.enabled = false;
        roofCam.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            frontCam.enabled = !frontCam.enabled;
            roofCam.enabled = !roofCam.enabled;
        }
    }
}
