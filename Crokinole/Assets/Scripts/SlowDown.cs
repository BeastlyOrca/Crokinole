using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{


    private bool isPulling;

    private Rigidbody puckRigidbody;

    private Vector3 velocityRef = Vector3.zero;
    private Vector3 angularVelocityRef = Vector3.zero;



    void Start()
    {
        puckRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        // Gradually slow down velocity and angular velocity
        puckRigidbody.velocity = Vector3.SmoothDamp(puckRigidbody.velocity, Vector3.zero, ref velocityRef, 0.9f);
        puckRigidbody.angularVelocity = Vector3.SmoothDamp(puckRigidbody.angularVelocity, Vector3.zero, ref angularVelocityRef, 0.9f);
  

        // When the puck has come to rest (both linear and angular velocity are small enough)
        if (puckRigidbody.velocity.magnitude < 0.1f && puckRigidbody.angularVelocity.magnitude < 0.1f)
        {
            
            stopMovement(); // Fully stop to prevent drifting
        }

        /**
        // Gradually reset rotation to upright if it's not moving
        if (puckRigidbody.angularVelocity.magnitude < 0.05f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2.5f);
        }
        */
        
    }


    void stopMovement() 
    {
        puckRigidbody.velocity = Vector3.zero; // stop velocity
        puckRigidbody.angularVelocity = Vector3.zero; // Stop rotation
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 3f);

        //transform.rotation = Quaternion.identity; // Reset rotation to (0,0,0)
        // Fully reset rotation
        transform.rotation = Quaternion.identity;
    }
}
