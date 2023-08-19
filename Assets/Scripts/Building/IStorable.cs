using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorable
{
    void StoreResource(ResourceType type, int amount);
    int GetStoredAmount(ResourceType type);
    EntryPointMarker GetClosestEntryPoint(Vector3 position);
}