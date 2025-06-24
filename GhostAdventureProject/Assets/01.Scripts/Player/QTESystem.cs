using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : Singleton<QTESystem>
{
    private IPossessable currentPossessable;
    // public QTERotatingBar qTERotatingBar;
    public TestQTEUI testQTEUI;

    public void StartQTE(IPossessable target)
    {
        currentPossessable = target;
        Debug.Log("Starting QTE");
        // qTERotatingBar.StartQTE();
        testQTEUI.StartQTE();
        
        // 임시 성공 처리
        // HandleQTEResult(true);
    }

    public void HandleQTEResult(bool success)
    {
        if (success)
        {
            Debug.Log("QTE succeeded");
            if(currentPossessable is RatPossessable rat)
                rat.OnQTESuccess();
        }
        else
        {
            Debug.Log("QTE failed");
            SoulEnergySystem.Instance.Consume(1);
        }
    }
}
