using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float velocity = 0;

    Rigidbody myRB;
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = myRB.velocity.magnitude;
        Debug.Log(velocity);
    }
}
