using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckID : MonoBehaviour
{
    public enum Player { Player1, Player2, Player3, Player4 }

    [Header("Owner")]
    public Player owner;

    [Header("Shot / State")]
    public bool canShoot = true;
    public bool validShot = false;
    public bool insideMiddle = false;

    public bool hasStopped = false; // true once puck comes to rest
    public float stopThreshold = 0.1f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Track stopped state independently of Slingshot
        if (rb.velocity.magnitude < stopThreshold && rb.angularVelocity.magnitude < stopThreshold)
        {
            hasStopped = true;
        }
        else
        {
            hasStopped = false;
        }
    }

   
}
