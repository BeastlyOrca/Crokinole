using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OuterCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P1 entered the outer circle!");
            // You can trigger scoring logic here
        }

        if (other.CompareTag("Player2")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P2 entered the outer circle!");
            // You can trigger scoring logic here
        }

        if (other.CompareTag("Player3")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P3 entered the outer circle!");
            // You can trigger scoring logic here
        }
        
        if (other.CompareTag("Player4")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P4 entered the outer circle!");
            // You can trigger scoring logic here
        }
    }
}
