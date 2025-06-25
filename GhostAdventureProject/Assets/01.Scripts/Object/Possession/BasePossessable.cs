using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePossessable : MonoBehaviour
{
    public GameObject interactionInfo;
    protected bool isPossessed = false;

    void Update()
    {
        if (!isPossessed)
            return;
        
        if(Input.GetKeyDown(KeyCode.E))
            Unpossess();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"트리거 충돌: {collision.name}");
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("플레이어 진입 감지됨");
            PossessionSystem.Instance.SetInteractTarget(GetComponent<BasePossessable>());
        }
        // 상호작용키 메시지 ON
        interactionInfo.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            PossessionSystem.Instance.ClearInteractionTarget(GetComponent<BasePossessable>());
        }
        // 상호작용키 메시지 OFF
        interactionInfo.SetActive(false);
    }

    public void OnTryPossess()
    {
        PossessionSystem.Instance.TryPossess(this);
    }

    public void RequestPossession()
    {
        Debug.Log($"{name} 빙의 시도 - QTE 호출");
        PossessionQTESystem.Instance.StartQTE(this);
    }

    public void Unpossess()
    {
        Debug.Log("빙의 해제");
        isPossessed = false;
        //PossessionStateManager.Instance.UnPossess();
    }


    public void OnQTESuccess()
    {
        Debug.Log("QTE 성공 - 빙의 완료");
        isPossessed = true;
        PossessionStateManager.Instance.StartPossessionTransition(this);
    }

    public void OnQTEFailure()
    {
        Debug.Log("QTE 실패 - 빙의 취소");
        isPossessed = false;
        Unpossess();
    }

    public virtual void IsPossessed(bool isPossessed)
    {
        this.isPossessed = isPossessed;
        /// 각 빙의오브젝트가 override해서 각자 기능 구현
    }
}
