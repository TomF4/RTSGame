using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public ParticleSystem clickEffect;

    public ObjectSelector objectSelector;
    
    private Camera cam;
    private GameObject selectedObject;
    private ISelectable selectedCharacter;

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

        ISelectable newCharacter = selectedObject?.GetComponent<ISelectable>();

        if (newCharacter != selectedCharacter)
        {
            selectedCharacter?.OnDeselect();
            selectedCharacter = newCharacter;
            selectedCharacter?.OnSelect();
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

                worker.ManualMoveTo(hit.point);
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
                    moveable.ManualMoveTo(hit.point);
                }
            }
        }
    }
}