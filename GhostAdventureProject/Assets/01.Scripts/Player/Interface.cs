using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionTarget
{
    void Interact();
}

public interface IPossessable
{
    bool Possess();
    void Unpossess();
}
