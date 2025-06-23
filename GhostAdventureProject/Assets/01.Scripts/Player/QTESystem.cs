using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : Singleton<QTESystem>
{
    private IPossessable currentPossessable;

    public void StartQTE(IPossessable target)
    {
        currentPossessable = target;
        Debug.Log("Starting QTE");
    }

    public void OnQTESuccess()
    {
        Debug.Log("QTE success");
    }

    public void OnQTEFail()
    {
        Debug.Log("QTE failed");
    }
}
