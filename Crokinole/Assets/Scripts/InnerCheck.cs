using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InnerCheck : MonoBehaviour
{

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P1 entered the inner circle!");
            // You can trigger scoring logic here
        }

        if (other.CompareTag("Player2")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P2 entered the inner circle!");
            // You can trigger scoring logic here
        }

        if (other.CompareTag("Player3")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P3 entered the inner circle!");
            // You can trigger scoring logic here
        }

        if (other.CompareTag("Player4")) // Ensure pucks have the tag "Puck"
        {
            Debug.Log("P4 entered the inner circle!");
            // You can trigger scoring logic here
        }

    }
    */



    private void OnTriggerStay(Collider other)
    {
        SlowDown slowDown = other.GetComponent<SlowDown>();
        if (slowDown != null && slowDown.isStopped)
        {
            //Debug.Log($"{other.tag} stopped in the inner circle!");
            // scoring logic here
        }

        Slingshot puckScript = other.GetComponent<Slingshot>();
        if (puckScript != null && slowDown.isStopped)
        {
            puckScript.insideMiddle = true;
            // scoring logic here
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Slingshot puckScript = other.GetComponent<Slingshot>();
        if (puckScript != null)
        {
            puckScript.insideMiddle = false;
            //Debug.Log($"{other.tag} left the inner circle.");
        }
    }
        

}
