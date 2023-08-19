using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WorkerBase : MonoBehaviour, IMoveable, ISelectableCharacter
{
    protected NavMeshAgent agent;
    
    public int carryingCapacity;
    
    private int carryingAmount;

    private ResourceManager resourceManager;
    private Resource targetResource;

    public int gatheringRate = 1;
    public float gatheringRange = 443.0f;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        // Check if the worker is within range of the target resource
        if (targetResource != null && Vector3.Distance(transform.position, targetResource.transform.position) 
            <= gatheringRange)
        {
            Debug.Log("gathering");
            GatherResource();
        }
    }

    public abstract void MoveTo(Vector3 destination);

    public void OnSelect()
    { 
    }

    public void OnDeselect()
    {
    }
    
    public void AssignResource(Resource resource)
    {
        targetResource = resource;
        Debug.Log($"Select resource: {targetResource.name}");
    }

    private void GatherResource()
    {
        // Logic to gather the resource over time
        carryingAmount += Mathf.FloorToInt(Time.deltaTime * gatheringRate);
        carryingAmount = Mathf.Min(carryingAmount, carryingCapacity);
        
        // If carrying capacity reached, maybe return resources
        if (carryingAmount >= carryingCapacity)
        {
            ReturnResources();
        }

        // Reduce quantity in targetResource
        targetResource.Gather(gatheringRate);
    }

    private void ReturnResources()
    {
        // Logic to return resources to storage and update ResourceManager
    }
}