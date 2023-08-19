using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageTest : BuildingBase, IStorable, IEnterable
{
    private Dictionary<ResourceType, int> storedResources = new Dictionary<ResourceType, int>();
    public List<EntryPointMarker> entryPointMarkers = new List<EntryPointMarker>();

    private void Start()
    {
        // Initialize the dictionary with resource types set to zero.
        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            storedResources[type] = 0;
        }
    }

    public override void OnBuildingInteracted()
    {
        // Logic when player interacts with this building.
        // For example, display an inventory or resource summary.
    }

    public void StoreResource(ResourceType type, int amount)
    {
        // Add the given amount to the respective resource in the dictionary.
        if(storedResources.ContainsKey(type))
        {
            storedResources[type] += amount;
        }
        else
        {
            Debug.LogError($"Resource type {type} not found in storage.");
        }
    }

    public int GetStoredAmount(ResourceType type)
    {
        // Return the stored amount of the resource or 0 if not found.
        int amount = 0;
        storedResources.TryGetValue(type, out amount);
        return amount;
    }

    public EntryPointMarker GetClosestEntryPoint(Vector3 position)
    {
        // Find the closest entry point to the given position.
        EntryPointMarker closestMarker = null;
        float shortestDistance = float.MaxValue;

        foreach (EntryPointMarker marker in entryPointMarkers)
        {
            float distance = Vector3.Distance(position, marker.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                closestMarker = marker;
            }
        }

        return closestMarker;
    }

    public void EnterBuilding()
    {
        // Logic for when an agent enters the building.
        // This might be used for visuals or gameplay mechanics.
    }

    public EntryPointMarker GetEntryPoint()
    {
        // Just return the first entry point or a default one. 
        // This can be expanded upon depending on your needs.
        if (entryPointMarkers.Count > 0)
        {
            return entryPointMarkers[0];
        }
        else
        {
            Debug.LogWarning("No entry point found for building.");
            return null;
        }
    }
}
