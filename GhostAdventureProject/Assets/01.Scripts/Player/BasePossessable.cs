using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePossessable : MonoBehaviour, IInteractionTarget, IPossessable
{
    protected bool isPossessed = false;
    public virtual void Interact()
    {
        if (PossessionStateManager.Instance.IsPossessing()) return; // 빙의 중일 때 상호작용 차단
        PossessionSystem.Instance.TryPossess(this);
    }

    public virtual void RequestPossession() // 빙의 요청
    {
        Debug.Log($"{name} 빙의 시도 - QTE 호출");
        QTESystem.Instance.StartQTE(this);
    }

    public virtual void RequestUnpossess()
    {
        Debug.Log("빙의 해제");
        isPossessed = false;
        PossessionStateManager.Instance.StartUnpossessTransition();
    }
    
    public virtual void OnQTESuccess()
    {
        Debug.Log("QTE 성공: 기본 빙의 성공 처리");
    }
    
    public void SetPossessed(bool value)
    {
        isPossessed = value;
    }

    void Update()
    {
        if (!isPossessed)
            return;
        
        if(Input.GetKeyDown(KeyCode.E))
            RequestUnpossess();
    }
}
