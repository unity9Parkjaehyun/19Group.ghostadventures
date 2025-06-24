using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPossessable : AnimalPossessable
{
    public void OnQTESuccess()
    {
        isPossessed = true;
        PossessionStateManager.Instance.Possess(GameManager.Instance.Player, this.gameObject);
    }
}
