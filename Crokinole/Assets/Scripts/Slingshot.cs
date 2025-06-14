using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for checking UI clicks

// IDEAS: ADD SINGLE PLAYER LEVELS WHERE PUCKS ARE ALREADY PLACED AND THEY NEED TO HIT A TARGET HIGH SCORE
public class Slingshot : MonoBehaviour
{
    public float maxPullDistance = 2f; // Maximum distance the puck can be pulled
    public float slingshotStrength = 10f; // Strength of the slingshot effect

    public bool isPulling = false;
    private bool isMovingPuck = false; // NEW: Whether we are freely moving the puck

    private Vector3 startPosition;
    private Vector3 pullPosition; // Store the "pulled back" position
    private Vector3 oppPosition;
    private Rigidbody puckRigidbody;

    private Camera mainCamera;
    private Vector3 velocityRef = Vector3.zero;
    private Vector3 angularVelocityRef = Vector3.zero;


    public LineRenderer lineRenderer; // Reference to the LineRenderer
    public ShootingZone area;
    public BoxCollider movementBounds1;
    public BoxCollider movementBounds2;
    public GameObject p1A;
    public GameObject p1B;
    





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
            oppPosition  = startPosition - pullDirection * pullDistance;   

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

            // Normalize the force direction again to ensure it's a unit vector
            forceDirection.Normalize();

            float forceMagnitude = Vector3.Distance(pullPosition, startPosition) * slingshotStrength;

            // Apply the force to the puck
            puckRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);

            // Reset the pull position and disable the line renderer
            pullPosition = startPosition;
            lineRenderer.enabled = false;
        }
    }

    void UpdateStartPosition()
    {
        // Update the starting position to the puck's current position when it stops moving
        startPosition = transform.position;
    }


    void stopMovement() {
        puckRigidbody.velocity = Vector3.zero; // stop velocity
        puckRigidbody.angularVelocity = Vector3.zero; // Stop rotation
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 3f);

        //transform.rotation = Quaternion.identity; // Reset rotation to (0,0,0)
        // Fully reset rotation
        transform.rotation = Quaternion.identity;
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

            Debug.Log("start");
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
