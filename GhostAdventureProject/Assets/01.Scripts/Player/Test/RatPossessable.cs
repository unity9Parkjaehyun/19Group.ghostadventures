using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPossessable : AnimalPossessable
{
    public void OnQTESuccess()
    {
        PossessionStateManager.Instance.Possess(GameManager.Instance.GetPlayer(), this.gameObject);
    }
}
