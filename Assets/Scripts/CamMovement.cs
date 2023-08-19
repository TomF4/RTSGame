using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 0f;
    public float scrollSpeed = 20f;
    public float minY = 20f;
    public float maxY = 120f;
    public float rotationSpeed = 50f;
    public float dragSpeed = 0.3f;
    public float smoothTime = 1.3f;  // Smoothing time for scrolling
    public float tiltSpeed = 50f; // Speed for tilting the camera
    
    public float startingXRotation = 45f;
    public float startingYPosition = 0;
    
    private float currentXRotation;
    private float targetY; // Target Y position
    private Vector3 currentPos;

    void Start() 
    {
        currentXRotation = startingXRotation;
        transform.rotation = Quaternion.Euler(startingXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);   
        targetY = startingYPosition;
    }

    void Update()
    {
        currentPos = transform.position;

        HandlePanning();
        HandleRotation();
        HandleTilting();
        HandleScrolling();

        transform.position = currentPos;
    }

    private void HandlePanning()
    {
        // Keep directional keys along a plane.
        Vector3 forward = transform.forward;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = transform.right;
        right.y = 0;
        right.Normalize();

        // prevent mouse panning interfere on dragging
        if (Input.GetMouseButton(2))
        {
            currentPos -= right * Input.GetAxis("Mouse X") * dragSpeed;
            currentPos -= forward * Input.GetAxis("Mouse Y") * dragSpeed;
        }
        else
        {
            if (Input.GetKey("w"))
            {
                currentPos += forward * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s"))
            {
                currentPos -= forward * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") )
            {
                currentPos += right * panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a"))
            {
                currentPos -= right * panSpeed * Time.deltaTime;
            }
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKey("q"))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey("e"))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    private void HandleTilting()
    {
        if (Input.GetKey("r"))
        {
            currentXRotation -= tiltSpeed * Time.deltaTime; // Tilt up
        }
        if (Input.GetKey("f"))
        {
            currentXRotation += tiltSpeed * Time.deltaTime; // Tilt down
        }
        
        // Clamping to avoid flipping over
        currentXRotation = Mathf.Clamp(currentXRotation, -80f, 80f);
        transform.rotation = Quaternion.Euler(currentXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void HandleScrolling()
    {   
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetY -= scroll * scrollSpeed * 100f * Time.deltaTime;
        targetY = Mathf.Clamp(targetY, minY, maxY);
        
        currentPos.y = Mathf.Lerp(currentPos.y, targetY, smoothTime);

        float threshold = 5;
        if (currentPos.y <= minY + threshold)
        {
            float percentToMin = (currentPos.y - minY) / threshold;
            float minRotationX = 10; 
            float targetRotationX = Mathf.Lerp(minRotationX, currentXRotation, percentToMin);
            transform.rotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}