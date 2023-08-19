using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoldierBase : MonoBehaviour, IMoveable, ISelectableCharacter
{
    protected UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public abstract void MoveTo(Vector3 destination);

    public void OnSelect()
    {
    }

    public void OnDeselect()
    {
    }
}