using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuboidWorker : WorkerBase
{
    public override void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
