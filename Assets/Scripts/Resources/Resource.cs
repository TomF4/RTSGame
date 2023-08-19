using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceType type;
    public int quantity;

    public int Gather(int gatherAmount)
    {
        int gathered = Mathf.Min(quantity, gatherAmount);
        quantity -= gathered;
        Debug.Log("Gathered");
        return gathered;
    }
    // add more properties like a model, collider  etc.
}