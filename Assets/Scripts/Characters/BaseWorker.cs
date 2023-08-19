using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class WorkerBase : MonoBehaviour, IMoveable, ISelectable
{
    protected NavMeshAgent agent;
    
    public int carryingCapacity;
    
    public float gatherTime = 1.0f; 
    public int gatherAmount = 1;
    public float gatheringRange = 5.0f;
    
    [SerializeField]
    private int currentCarryingAmount;
    private float currentGatherTime; 

    private ResourceManager resourceManager;
    private EntryPointMarker targetEntryPointMarker;
    private IStorable targetStorage;

    protected Resource targetResource;
    protected WorkerState currentState = WorkerState.Idle;
    
    private static IStorable defaultStorage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentGatherTime = gatherTime;
        if (defaultStorage == null)
        {
            SetDefaultStorage();
        }
        targetStorage = defaultStorage;
        targetEntryPointMarker = targetStorage.GetClosestEntryPoint(this.transform.position);
    }
    
    private void Update()
    {
        switch (currentState)
        {
            case WorkerState.Gathering:
                HandleGatheringBehaviour();
                break;
            case WorkerState.ReturningResources:
                HandleReturningBehaviour();
                break;
            // ... Any other states you wish to manage.
        }
    }

    public abstract void ManualMoveTo(Vector3 destination);
    public abstract void MoveTo(Vector3 destination);

    private void SetDefaultStorage()
    {
        defaultStorage = GameManager.GetDefaultStorage();
    }

    public void AssignToStorage(IStorable storage)
    {
        targetStorage = storage;
        targetEntryPointMarker = storage.GetClosestEntryPoint(this.transform.position);
    }

    public void OnSelect()
    { 
    }

    public void OnDeselect()
    {
    }
    
    public void AssignResource(Resource resource)
    {
        targetResource = resource;
        currentState = WorkerState.Gathering;
        Debug.Log($"Select resource: {targetResource.name}");
    }

    private void HandleGatheringBehaviour()
    {
        if (targetResource != null && Vector3.Distance(transform.position, targetResource.transform.position) <= gatheringRange)
        {
            currentGatherTime -= Time.deltaTime;
            
            if (currentGatherTime <= 0)
            {
                GatherResource();
                currentGatherTime = gatherTime;
            }
        }
    }

    private void HandleReturningBehaviour()
    {
        if(Vector3.Distance(transform.position, targetEntryPointMarker.transform.position) <= 5f)
        {
            Debug.Log("storing");
            StoreResources();
            Debug.Log("returning");
            ReturnToAssignedResource();
        }
    }

    private void GatherResource()
    {
        // Calculate the maximum amount the worker can gather without exceeding capacity
        int amountToGather = Mathf.Min(gatherAmount, carryingCapacity - currentCarryingAmount);
        int actuallyGathered = targetResource.Gather(amountToGather);
        if (actuallyGathered <= 0)
        {
            targetResource = null;
            Debug.Log("no more resources");
        }
        currentCarryingAmount += actuallyGathered;

        Debug.Log("gather");


        if (currentCarryingAmount >= carryingCapacity)
        {
            currentState = WorkerState.ReturningResources;
            agent.SetDestination(targetEntryPointMarker.transform.position);
        }
    }

    private void StoreResources()
    {
        targetStorage.StoreResource(targetResource.type, currentCarryingAmount);
        currentCarryingAmount = 0;
    }

    private void ReturnToAssignedResource()
    {
        if(targetResource != null)
        {
            MoveTo(targetResource.transform.position);
            currentState = WorkerState.Gathering;
        }
        else
        {
            currentState = WorkerState.Idle;
        }
    }
}