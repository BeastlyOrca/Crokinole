using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slingshot : MonoBehaviour
{
    // =========================
    // Slingshot Settings
    // =========================
    [Header("Slingshot Settings")]
    [SerializeField] private float maxPullDistance = 2f;    // Maximum distance the puck can be pulled
    [SerializeField] private float slingshotStrength = 10f; // Strength of the slingshot effect

    // =========================
    // Puck Movement
    // =========================
    [Header("Puck Movement")]
    [SerializeField] private bool isMovingPuck = false; // Whether we are freely moving the puck (reposition, not shoot)
    private bool isPulling = false;

    private Vector3 startPosition;
    private Vector3 pullPosition; // Store the "pulled back" position
    private Vector3 oppPosition;
    private Rigidbody puckRigidbody;

    private Vector3 velocityRef = Vector3.zero;
    private Vector3 angularVelocityRef = Vector3.zero;

    // =========================
    // References
    // =========================
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;      // Reference to the LineRenderer
    [SerializeField] private ShootingZone area;
    [SerializeField] private BoxCollider movementBounds1;
    [SerializeField] private BoxCollider movementBounds2;
    [SerializeField] private GameObject p1A;
    [SerializeField] private GameObject p1B;

    // =========================
    // Shot Tracking
    // =========================
    [Header("Shot Tracking")]
    [SerializeField] private bool canShoot = true;    // Only allow shooting when true
    [SerializeField] private bool validShot = false;  // Track if we hit an opponent
    [SerializeField] public bool insideMiddle = false;

    // =========================
    // Owner
    // =========================
    [Header("Owner Settings")]
    [SerializeField] public Player owner; // Player ownership
    public enum Player { Player1, Player2, Player3, Player4 }
    private Camera mainCamera;





    void Start()
    {
        puckRigidbody = this.GetComponent<Rigidbody>();
        startPosition = transform.position; // Store the initial position of the puck
        mainCamera = Camera.main; // Cache the main camera

        // Ensure LineRenderer is properly set up
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned!");
        }
        else
        {
            lineRenderer.positionCount = 2; // Two points: start and pull position

            lineRenderer.enabled = false;  // Disable by default
        }

        // when instantiating new player, this throws an error, harmless FOR NOW
        p1A.SetActive(false);
        p1B.SetActive(false);

    }

    void Update()
    {
        if (isMovingPuck)
        {
            MovePuckWithMouse();
            return; // Prevents slingshot behavior from running
        }

        HandleInput();


        // When the puck has come to rest (both linear and angular velocity are small enough)
        if (!isPulling && puckRigidbody.velocity.magnitude < 0.1f && puckRigidbody.angularVelocity.magnitude < 0.1f)
        {
            UpdateStartPosition();
        }

        /**
        // Gradually reset rotation to upright if it's not moving
        if (puckRigidbody.angularVelocity.magnitude < 0.05f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2.5f);
        }
        */
    }


    void HandleInput()
    {

        if (!canShoot) return; // Ignore input if shooting is disabled

        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            StartPull();
        }
        else if (Input.GetMouseButton(0)) // Left mouse button held
        {
            ContinuePull();
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            ReleasePull();
        }
    }

    void StartPull()
    {
        // Check if the mouse clicked this puck
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object is this puck
            if (hit.transform == this.transform)
            {
                isPulling = true;
                //puckRigidbody.isKinematic = true; // Temporarily disable physics
                lineRenderer.enabled = true; // Enable the line renderer
            }
        }
    }

    void ContinuePull()
    {
        if (isPulling)
        {
            // Convert mouse position to world coordinates
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.y - transform.position.y));
            mousePosition.y = startPosition.y; // Lock the Y position to the puck's plane

            // Calculate the pull direction and distance
            Vector3 pullDirection = (mousePosition - startPosition).normalized;
            float pullDistance = Mathf.Clamp(Vector3.Distance(mousePosition, startPosition), 0, maxPullDistance);

            // Store the pull position (used for release calculation)
            pullPosition = startPosition + pullDirection * pullDistance;
            oppPosition = startPosition - pullDirection * pullDistance;

            // Update the LineRenderer positions
            lineRenderer.SetPosition(0, startPosition); // Start of the line
            lineRenderer.SetPosition(1, oppPosition);  // End of the line
        }
    }

    void ReleasePull()
    {
        if (isPulling)
        {
            isPulling = false;

            //puckRigidbody.isKinematic = false; // Re-enable physics
            // Calculate the force to apply
            Vector3 forceDirection = (startPosition - pullPosition).normalized;

            // Set the Y component of the force direction to 0 to keep it horizontal
            forceDirection.y = 0;

            // Normalize the force direction again to ensure it's a unit vector and Apply the force to the puck
            forceDirection.Normalize();
            float forceMagnitude = Vector3.Distance(pullPosition, startPosition) * slingshotStrength;
            puckRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);


            // Reset the pull position and disable the line renderer + canShoot
            pullPosition = startPosition;
            canShoot = false;
            lineRenderer.enabled = false;
        }
    }

    void UpdateStartPosition()
    {
        // Update the starting position to the puck's current position when it stops moving
        startPosition = transform.position;
    }



    // 🔹 Toggle Move Mode via Button
    public void ToggleMoveMode()
    {
        p1A.SetActive(true);
        p1B.SetActive(true);
        isMovingPuck = !isMovingPuck;

        if (isMovingPuck)
        {
            //puckRigidbody.isKinematic = true; // Disable physics interactions
            puckRigidbody.velocity = Vector3.zero;
            puckRigidbody.angularVelocity = Vector3.zero;


        }
    }


    // 🔹 Button: Confirm & Return to Slingshot Mode
    public void ConfirmPosition()
    {
        p1A.SetActive(false);
        p1B.SetActive(false);
        isMovingPuck = false;

        //puckRigidbody.isKinematic = false; // Re-enable physics
        startPosition = transform.position; // Update slingshot starting point
    }



    /**
    this function will fuck with you later i can already tell. It clamps the position to one or the other cubes based on the 
    x value. Since p1 moves how it does, you can easy check this. For the other players it wont be x < 1, it may be z values
    this is now fine and working, just mess with the scales
    */
    void MovePuckWithMouse()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = startPosition.y;


                if (movementBounds1 != null && movementBounds2 != null)
                {

                    Vector3 clampedPosition;

                    if (this.transform.position.x < 1)
                    {
                        Vector3 p2 = movementBounds2.ClosestPoint(targetPosition);
                        float dist = Vector3.Distance(targetPosition, p2);
                        clampedPosition = movementBounds2.ClosestPoint(targetPosition);

                    }
                    else
                    {
                        // Clamp to the closest point in either bound
                        Vector3 p1 = movementBounds1.ClosestPoint(targetPosition);


                        float dist = Vector3.Distance(targetPosition, p1);
                        clampedPosition = movementBounds1.ClosestPoint(targetPosition);

                    }


                    clampedPosition.y = startPosition.y;
                    transform.position = clampedPosition;
                }
            }
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        // Try to get the Slingshot/puck script on the object we hit
        Slingshot otherPuck = collision.gameObject.GetComponent<Slingshot>();

        if (otherPuck != null)
        {
            // Check if we hit an opponent
            if (otherPuck.owner != this.owner)
            {
                // check if the opponent is inside the middle
                if (otherPuck.insideMiddle)
                {
                    validShot = true;
                    Debug.Log($"{owner} hit {otherPuck.owner}! inside the middle, Valid shot.");
                }
                
            }
        }
    }






    // this needs to be editied
    /*
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is another puck
        if (collision.gameObject.CompareTag("Puck")) // Ensure all pucks are tagged as "Puck"
        {
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (targetRigidbody != null)
            {
                // Only apply force if THIS puck is moving
                if (puckRigidbody.velocity.magnitude > 0.1f)
                {
                    // Calculate the impact force based on THIS puck's velocity
                    Vector3 impactForce = puckRigidbody.velocity * 2; // Adjust multiplier as needed

                    // Apply the force to the target puck
                    targetRigidbody.AddForce(impactForce, ForceMode.Impulse);

                    // Optionally: Reduce this puck's velocity slightly (optional realism tweak)
                    puckRigidbody.velocity *= 0.5f;
                }
            }
        }
    }
    */

}
