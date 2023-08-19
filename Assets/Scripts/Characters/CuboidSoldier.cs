using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidSoldier : SoldierBase
{
    public override void ManualMoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public override void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}