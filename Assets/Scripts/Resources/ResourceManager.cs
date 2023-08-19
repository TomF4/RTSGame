using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class to manager players stored materials
/// </summary>
public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceType, int> resources;

    private void Start()
    {
        resources = new Dictionary<ResourceType, int>
        {
            { ResourceType.Wood, 0 },
            { ResourceType.Stone, 0 },
            { ResourceType.Iron, 0 },
            { ResourceType.Money, 0 }
        };
    }

    public void AddResource(ResourceType type, int amount)
    {
        resources[type] += amount;
    }
    
    public int GetResource(ResourceType type)
    {
        return resources[type];
    }
}