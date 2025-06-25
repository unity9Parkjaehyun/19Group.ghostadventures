using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTESystem : Singleton<QTESystem>
{
    private IPossessable currentPossessable;
    // public QTERotatingBar qTERotatingBar;
    public TestQTEUI testQTEUI;

    private bool isRunning = false;
    public bool IsRunning() => isRunning;

    public void StartQTE(IPossessable target)
    {
        currentPossessable = target;
        Debug.Log("Starting QTE");
        // qTERotatingBar.StartQTE();
        isRunning = true;
        testQTEUI.StartQTE();

        // 임시 성공 처리
        // HandleQTEResult(true);
    }

    public void HandleQTEResult(bool success)
    {
        isRunning = false;
        if (success)
        {
            Debug.Log("QTE succeeded");
            if (currentPossessable != null)
                currentPossessable.OnQTESuccess();   // 모든 빙의 가능 오브젝트가 QTE 성공 시 실행
        }
        else
        {
            Debug.Log("QTE failed");
            SoulEnergySystem.Instance.Consume(1);
        }
        if (GameManager.Instance.Player != null)
        {
            var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
            if (playerController != null)
                playerController.ClearInteractionTarget(currentPossessable as IInteractionTarget);
        }
    }

}
