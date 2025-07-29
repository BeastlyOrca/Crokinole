using UnityEngine;
using UnityEngine.EventSystems;

public class TrainingDummy : MonoBehaviour
{
    public float speed = 10f;
    public Vector3 targetPos;
    public bool isMoving;
    private bool isDragging = false;

    const int MOUSE = 0;

    void Start()
    {
        targetPos = transform.position;
        isMoving = false;
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        // Begin drag only if mouse clicked on this object
        if (Input.GetMouseButtonDown(MOUSE))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    SetTargetPosition();
                }
            }
        }

        // While dragging
        if (Input.GetMouseButton(MOUSE) && isDragging)
        {
            SetTargetPosition();
        }

        // Stop dragging
        if (Input.GetMouseButtonUp(MOUSE))
        {
            isDragging = false;
        }

        // Move toward the target position
        if (isMoving)
        {
            MoveObject();
        }
    }

    void SetTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float distance))
        {
            targetPos = ray.GetPoint(distance);
            targetPos.y = transform.position.y; // Lock Y
            isMoving = true;
        }
    }

    void MoveObject()
    {
        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
            isMoving = false;

        Debug.DrawLine(transform.position, targetPos, Color.red);
    }
}
