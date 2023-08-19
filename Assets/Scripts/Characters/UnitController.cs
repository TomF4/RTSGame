using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public ParticleSystem clickEffect;

    public ObjectSelector objectSelector;
    
    private Camera cam;
    private GameObject selectedObject; // Keep track of the selected GameObject
    private ISelectableCharacter selectedCharacter;

    private void Start()
    {
        cam = Camera.main;    
    }

    private void Update()
    {
        HandleSelection();
        HandleMovement();
        HandleAssignment();
    }

    private void HandleSelection()
    {
        selectedObject = objectSelector.GetSelectedObject();

        ISelectableCharacter newCharacter = selectedObject?.GetComponent<ISelectableCharacter>();

        if (newCharacter != selectedCharacter)
        {
            selectedCharacter?.OnDeselect();
            selectedCharacter = newCharacter;
            selectedCharacter?.OnSelect();
        }

        if (selectedCharacter is WorkerBase)
        {
            var worker = selectedObject?.GetComponent<WorkerBase>();
            
            // worker.AssignResource();
        }

    }

    private void HandleAssignment()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var worker = selectedObject?.GetComponent<WorkerBase>();
            if (worker is null)
                return;
                
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!hit.transform.GetComponent<Resource>())
                {
                    return;
                }

                clickEffect.transform.position = hit.point;
                clickEffect.Play();

                worker.MoveTo(hit.point);
                worker.AssignResource(hit.transform.GetComponent<Resource>());
            }
        }
    }

    private void HandleMovement()
    {
        if (selectedObject != null)
        {
            IMoveable moveable = selectedObject?.GetComponent<IMoveable>();
            if (moveable != null && Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    clickEffect.transform.position = hit.point;
                    clickEffect.Play();
                    moveable.MoveTo(hit.point);
                }
            }
            else
            {
                // some logic here if the selected object is not IMoveable?
                
            }
        }
    }
}