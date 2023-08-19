using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<IStorable> allStorages = new List<IStorable>();
    private IStorable defaultStorage;

    private void Awake()
    {
        // Singleton Pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        InitializeStorages();
    }

    private void InitializeStorages()
    {
        allStorages.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IStorable>());
        
        if (allStorages.Count > 0)
        {
            defaultStorage = allStorages[0];  
        }
    }

    public static IStorable GetDefaultStorage()
    {
        return instance.defaultStorage;
    }

    public static IStorable GetClosestStorage(Vector3 position)
    {
        IStorable closestStorage = null;
        float shortestDistance = float.MaxValue;

        foreach (IStorable storage in instance.allStorages)
        {
            float distance = Vector3.Distance(position, (storage as MonoBehaviour).transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestStorage = storage;
            }
        }

        return closestStorage;
    }

    // Add any other methods or properties that are global in nature and should be managed by the GameManager.
}
