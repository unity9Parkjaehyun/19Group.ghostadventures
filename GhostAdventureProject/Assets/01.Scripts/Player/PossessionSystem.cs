using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionSystem : Singleton<PossessionSystem>
{
    public bool TryPossess(IPossessable target)
    {
        return target.Possess();
    }
}
