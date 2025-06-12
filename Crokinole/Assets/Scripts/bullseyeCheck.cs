using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullseyeCheck : MonoBehaviour
{
    public bool isFullyOnTop = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puck"))
        {
            CheckIfFullyOnTop(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Puck"))
        {
            CheckIfFullyOnTop(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Puck"))
        {
            isFullyOnTop = false;
            Debug.Log("A puck has left the bullseye.");
        }
    }

    private void CheckIfFullyOnTop(Collider puck)
    {
        // Get the radius of the larger cylinder's top surface
        float bullseyeRadius = GetComponent<Collider>().bounds.extents.x;

        // Get the radius of the incoming puck
        float puckRadius = puck.bounds.extents.x;

        // Get the center of the bullseye's top surface
        Vector3 bullseyeCenter = transform.position;
        bullseyeCenter.y += GetComponent<Collider>().bounds.extents.y;

        // Get the center of the puck
        Vector3 puckCenter = puck.transform.position;

        // Calculate 2D distance (XZ plane)
        Vector2 bullseye2D = new Vector2(bullseyeCenter.x, bullseyeCenter.z);
        Vector2 puck2D = new Vector2(puckCenter.x, puckCenter.z);
        float distance = Vector2.Distance(bullseye2D, puck2D);

        // Check if puck is fully inside the bullseye top
        if (distance + puckRadius <= bullseyeRadius)
        {
            if (!isFullyOnTop)
            {
                isFullyOnTop = true;
                Debug.Log("Puck is fully on the bullseye!");
            }
        }
        else
        {
            if (isFullyOnTop)
            {
                isFullyOnTop = false;
                Debug.Log("Puck is no longer fully on the bullseye.");
            }
        }
    }
}
