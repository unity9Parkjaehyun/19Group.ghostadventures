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

    public virtual void Possess()
    {
        Debug.Log($"{name} 빙의 시도 - QTE 호출");
        QTESystem.Instance.StartQTE(this);
    }

    public virtual void Unpossess()
    {
        Debug.Log("빙의 해제");
        isPossessed = false;
        PossessionStateManager.Instance.UnPossess();
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
            Unpossess();
    }
}
