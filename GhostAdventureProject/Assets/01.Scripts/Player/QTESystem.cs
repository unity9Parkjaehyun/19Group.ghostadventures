using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : Singleton<QTESystem>
{
    private BasePossessable currentPossessable;
    // public QTERotatingBar qTERotatingBar;
    public TestQTEUI testQTEUI;
    
    private bool isRunning = false;
    public bool IsRunning() => isRunning;

    public void StartQTE(BasePossessable target) // 시작
    {
        currentPossessable = target;
        Debug.Log("Starting QTE");
        // qTERotatingBar.StartQTE();
        isRunning = true;
        testQTEUI.StartQTE();
        
        // 임시 성공 처리
        // HandleQTEResult(true);
    }

    public void HandleQTEResult(bool success) // 결과 처리
    {
        isRunning = false;
        if (success)
        {
            Debug.Log("QTE succeeded");
            currentPossessable.OnQTESuccess();
        }
        else
        {
            Debug.Log("QTE failed");
            SoulEnergySystem.Instance.Consume(1);
        }
    }
}
