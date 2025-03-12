using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullseyeCheck : MonoBehaviour
{
    public GameObject smallerCylinder; // Reference to the smaller cylinder
    private bool isFullyOnTop = false; // Track if the smaller cylinder is fully on top

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == smallerCylinder)
        {
            CheckIfFullyOnTop();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == smallerCylinder)
        {
            CheckIfFullyOnTop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == smallerCylinder)
        {
            isFullyOnTop = false;
            Debug.Log("Smaller cylinder has left the larger cylinder.");
        }
    }

    private void CheckIfFullyOnTop()
    {
        // Get the radius of the larger cylinder's top surface
        float largerCylinderRadius = GetComponent<Collider>().bounds.extents.x; // Assumes a circular top surface

        // Get the radius of the smaller cylinder
        float smallerCylinderRadius = smallerCylinder.GetComponent<Collider>().bounds.extents.x;

        // Get the center position of the larger cylinder's top surface
        Vector3 largerCylinderCenter = transform.position;
        largerCylinderCenter.y = transform.position.y + GetComponent<Collider>().bounds.extents.y; // Top surface

        // Get the center position of the smaller cylinder
        Vector3 smallerCylinderCenter = smallerCylinder.transform.position;

        // Calculate the distance between the centers in 2D (ignoring the Y-axis)
        Vector2 largerCenter2D = new Vector2(largerCylinderCenter.x, largerCylinderCenter.z);
        Vector2 smallerCenter2D = new Vector2(smallerCylinderCenter.x, smallerCylinderCenter.z);
        float distance = Vector2.Distance(largerCenter2D, smallerCenter2D);

        // Check if the smaller cylinder is fully within the larger cylinder's top circle
        if (distance + smallerCylinderRadius <= largerCylinderRadius)
        {
            if (!isFullyOnTop)
            {
                isFullyOnTop = true;
                Debug.Log("Smaller cylinder is fully within the perimeter of the larger cylinder's top circle!");
            }
        }
        else
        {
            if (isFullyOnTop)
            {
                isFullyOnTop = false;
                Debug.Log("Smaller cylinder is no longer fully within the perimeter.");
            }
        }
    }
}