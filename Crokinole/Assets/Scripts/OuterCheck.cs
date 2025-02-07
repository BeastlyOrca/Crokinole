using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OuterCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puck")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("Puck entered the outer circle!");
            // You can trigger scoring logic here
        }
    }
}
