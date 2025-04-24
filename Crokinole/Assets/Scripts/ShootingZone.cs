using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingZone : MonoBehaviour
{
    public bool _isTouching = false;
    public GameObject shootingPuck; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shootingPuck) // Check if it's the specific shooting puck
        {
            _isTouching = true;
            Debug.Log("Shooting Puck entered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == shootingPuck)
        {
            _isTouching = false;
            Debug.Log("Shooting Puck exited");
        }
    }

    public bool IsPuckTouching()
    {
        return _isTouching;
    }
}