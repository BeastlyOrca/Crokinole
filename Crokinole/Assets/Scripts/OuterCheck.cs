using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// something something puck keeps entering and exiting because physics is bullshit

public class OuterCheck : MonoBehaviour
{

    public GameManager gameManager;
    private int points = 5;

    /*
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
    */

    private void OnTriggerStay(Collider other)
    {
        SlowDown slowDown = other.GetComponent<SlowDown>();
        if (slowDown != null && slowDown.isStopped && !slowDown.countedOuter)
        {
            // Add points only once
            gameManager.UpdateOuterCount(other.tag, points);

            // Store points on puck
            slowDown.finalPosition = points;

            // Mark as counted so it doesn’t add repeatedly
            slowDown.countedOuter = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {


        SlowDown slowDown = other.GetComponent<SlowDown>();
        if (slowDown != null && slowDown.countedOuter)
        {
            // Report to GameManager
            gameManager.UpdateOuterCount(other.tag, -points);
            //slowDown.countedOuter = false;

        }
    }
}
