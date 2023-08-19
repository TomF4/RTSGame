using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectableCharacter
{
    // You can define common properties or methods that all selectable characters must have
    // For example, you could have a method to highlight the selected character
    void OnSelect();
    void OnDeselect();
}
