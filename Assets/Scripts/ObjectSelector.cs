using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public Material whiteFadingMaterial; // assign this in Unity editor

    private const float FadeSpeed = 0.25f;
    private const float MaxTransparency = 0.25f;
    private const float MinTransparency = 0.0f;

    private Camera cam;
    private GameObject selectedObject;
    private Renderer objectRenderer;
    private bool isFadingIn = true;

    private void Start() 
    {
        cam = Camera.main;
    }

    private void Update() 
    {
        HandleObjectSelection();
        ApplyFadeEffect();
    }
    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }
    
    private void HandleObjectSelection()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;
        
        if (selectedObject != null && hit.transform.gameObject == selectedObject) 
        {
            DeselectObject();
        }
        else 
        {
            SelectNewObject(hit.transform.gameObject);
        }
    }

    private void SelectNewObject(GameObject newObject)
    {
        if (selectedObject != null && objectRenderer != null) 
        {
            ResetObjectMaterial();
        }

        selectedObject = newObject;
        Debug.Log("Selected: " + selectedObject.name);

        objectRenderer = selectedObject.GetComponent<Renderer>();

        ApplyWhiteFadingMaterial();
    }

    private void DeselectObject()
    {
        ResetObjectMaterial();

        selectedObject = null;
        objectRenderer = null;
    }

    private void ResetObjectMaterial()
    {
        Material[] materials = objectRenderer.materials;
        materials[1] = null;
        objectRenderer.materials = materials;
    }

    private void ApplyWhiteFadingMaterial()
    {
        Material[] materials = new Material[2];
        materials[0] = objectRenderer.material;
        materials[1] = whiteFadingMaterial;
        objectRenderer.materials = materials;
    }

    private void ApplyFadeEffect()
    {
        if (selectedObject == null || objectRenderer == null) return;

        Color color = whiteFadingMaterial.color;
        float transparency = color.a;

        transparency = isFadingIn ? IncreaseTransparency(transparency) : DecreaseTransparency(transparency);

        color.a = transparency;
        whiteFadingMaterial.color = color;
    }

    private float IncreaseTransparency(float transparency)
    {
        transparency += Time.deltaTime * FadeSpeed;

        if (transparency >= MaxTransparency) 
        {
            transparency = MaxTransparency;
            isFadingIn = false;
        }

        return transparency;
    }

    private float DecreaseTransparency(float transparency)
    {
        transparency -= Time.deltaTime * FadeSpeed;

        if (transparency <= MinTransparency) 
        {
            transparency = MinTransparency;
            isFadingIn = true;
        }

        return transparency;
    }
}