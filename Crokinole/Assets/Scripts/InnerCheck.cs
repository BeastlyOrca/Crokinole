using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InnerCheck : MonoBehaviour
{

    public GameManager gameManager;
    private int points = 10;

    private void OnTriggerStay(Collider other)
    {
        SlowDown slowDown = other.GetComponent<SlowDown>();
        if (slowDown != null && slowDown.isStopped)
        {
            // Report to GameManager
            

            // Optional: store points on puck
            slowDown.finalPosition = points;

            // Mark so it doesn’t keep adding every frame
            slowDown.isStopped = false; // or another flag like "countedInInner"
        }

        PuckID puckScript = other.GetComponent<PuckID>();
        if (puckScript != null)
        {
            puckScript.insideMiddle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        

        Slingshot puckScript = other.GetComponent<Slingshot>();
        if (puckScript != null && puckScript.canShoot == false)
        {
            // Report to GameManager
            Debug.Log( other.tag + "left");
            gameManager.UpdateInnerCount(other.tag, (points * -1));
        }
    }
}
