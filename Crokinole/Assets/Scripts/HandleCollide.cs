using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollide : MonoBehaviour
{
    public float forceMultiplier = 2f; // Adjust the strength of the applied force

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding is the puck
        if (collision.gameObject.CompareTag("Puck")) // Ensure the puck has the tag "Puck"
        {
            // Get the Rigidbody of the target object (this object)
            Rigidbody targetRigidbody = GetComponent<Rigidbody>();

            if (targetRigidbody != null)
            {
                // Calculate and apply force based on the puck's velocity
                Vector3 impactForce = collision.gameObject.GetComponent<Rigidbody>().velocity * forceMultiplier;
                targetRigidbody.AddForce(impactForce, ForceMode.Impulse);
            }
        }
    }
}
