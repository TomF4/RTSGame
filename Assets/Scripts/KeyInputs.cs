using UnityEngine;

public class KeyInputs : MonoBehaviour
{
    public ObjectSelector objectSelector;

    private bool CameraLocked = false;

    void Update()
    {
        HandleKeyboardInputs();

        if (CameraLocked)
        {
            LockCamOnSelectedTarget();
        }
    }

    void HandleKeyboardInputs()
    {
        if (Input.GetKeyDown("y"))
        {
            CameraLocked = !CameraLocked;
        }
    }

    void LockCamOnSelectedTarget()
    {
        GameObject selectedObject = objectSelector.GetSelectedObject();
        if (selectedObject != null)
        {
            Vector3 targetPosition = selectedObject.transform.position;
            Vector3 cameraPosition = Camera.main.transform.position;

            cameraPosition.x = targetPosition.x;
            cameraPosition.z = targetPosition.z - 10f; // You can adjust the offset as needed

            Camera.main.transform.position = cameraPosition;
        }
    }
}