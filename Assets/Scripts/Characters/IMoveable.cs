using UnityEngine;

public interface IMoveable
{
    void MoveTo(Vector3 destination);
    void ManualMoveTo(Vector3 destination);
}

