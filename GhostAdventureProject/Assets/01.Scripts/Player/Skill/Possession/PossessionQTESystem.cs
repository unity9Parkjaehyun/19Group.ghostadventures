using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionQTESystem : Singleton<PossessionQTESystem>
{
    private QTEUI testQTEUI;
    private BasePossessable currentTarget;

    public void StartQTE(BasePossessable target)
    {
        currentTarget = target;
        Debug.Log("Starting QTE");
        testQTEUI.ShowQTEUI();
    }

    public void HandleQTEResult(bool success)
    {
        if (success)
        {
            Debug.Log("QTE succeeded");
            currentTarget?.OnQTESuccess();
        }
        else
        {
            Debug.Log("QTE failed");
            currentTarget?.OnQTEFailure();
        }
    }
}
