using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPossessable : MonoBehaviour, IInteractionTarget, IPossessable
{
    public void Interact() => PossessionSystem.Instance.TryPossess(this);

    public bool Possess()
    {
        Debug.Log("Possessing");
        QTESystem.Instance.StartQTE(this);
        return true;
    }

    public void Unpossess()
    {
        Debug.Log("Unpossessing");
    }
}
