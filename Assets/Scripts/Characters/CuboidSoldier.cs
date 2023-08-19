using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidSoldier : SoldierBase
{
     public override void MoveTo(Vector3 destination)
    {
        // Specific logic for moving MeleeSoldier
        agent.SetDestination(destination);
    }

    // Additional logic specific to MeleeSoldier, like melee attack
}