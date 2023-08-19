using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Resource : MonoBehaviour
{
    public ResourceType type; // Ensure you have this enum defined elsewhere

    [SerializeField] 
    private int quantity;

    public int Quantity 
    {
        get { return quantity; } 
        private set 
        {
            quantity = value;
            if(quantity <= 0)
            {
                OnResourceDepleted?.Invoke(this);
                // Optionally hide or destroy this gameObject
                // gameObject.SetActive(false); or Destroy(gameObject);
            }
        }
    }

    public UnityEvent<Resource> OnResourceDepleted;

    public int Gather(int gatherAmount)
    {
        if(gatherAmount < 0)
        {
            Debug.LogWarning("Invalid gather amount!");
            return 0;
        }

        int gathered = Mathf.Min(Quantity, gatherAmount);
        Quantity -= gathered;
        
        return gathered;
    }
}