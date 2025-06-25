using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionSystem : Singleton<PossessionSystem>
{
    public bool TryPossess(BasePossessable target)
    {
        if (!SoulEnergySystem.Instance.HasEnoughEnergy(3))
        {
            Debug.Log("Not enough energy");
            return false;
        }
        SoulEnergySystem.Instance.Consume(3);
        //target.RequestPossession();
        return true;
    }
}
