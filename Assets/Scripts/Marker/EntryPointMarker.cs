using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPointMarker : MonoBehaviour
{
    public BuildingBase associatedBuilding; 

    // If you want to have different types of markers (e.g., for entering, for storage)
    public enum MarkerType { Default, Entry, Storage }
    public MarkerType markerType = MarkerType.Default;

    // Other properties or methods for the entry point marker.
}