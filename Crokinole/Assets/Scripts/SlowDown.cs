using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{


    private bool isPulling;
    public bool isStopped;

    private Rigidbody puckRigidbody;

    private Vector3 velocityRef = Vector3.zero;
    private Vector3 angularVelocityRef = Vector3.zero;


    private Collider puckCollider;
    public float originalBounciness;

    void Start()
    {
        puckRigidbody = GetComponent<Rigidbody>();
        puckCollider = GetComponent<Collider>();

        if (puckCollider.material != null)
            originalBounciness = puckCollider.material.bounciness;
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
            // Temporarily remove bounciness
            if (puckCollider.material != null)
                puckCollider.material.bounciness = 0f;

            stopMovement(); // fully stop
        }
        else
        {
            isMove();
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
        Debug.Log("stopped");
        isStopped = true;

        
        
    }

    private IEnumerator RestoreBouncinessAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (puckCollider.material != null)
            puckCollider.material.bounciness = originalBounciness;

        Debug.Log("Bounciness restored");
    }



    public void isMove()
    {
        Debug.Log("is move");
        isStopped = false;

        // Restore original bounciness
        if (puckCollider.material != null)
        {
            puckCollider.material.bounciness = originalBounciness;

            puckRigidbody.useGravity = true;
            puckRigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
