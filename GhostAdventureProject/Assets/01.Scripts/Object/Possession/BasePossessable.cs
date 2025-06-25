using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePossessable : MonoBehaviour
{
    public GameObject interactionInfo;
    protected bool isPossessed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"트리거 충돌: {collision.name}");
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("플레이어 진입 감지됨");
            player.SetInteractTarget(GetComponent<BasePossessable>());
        }
        // 상호작용키 메시지 ON
        interactionInfo.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ClearInteractionTarget(GetComponent<BasePossessable>());
        }
        // 상호작용키 메시지 OFF
        interactionInfo.SetActive(false);
    }

    public void Interact()
    {
        PossessionSystem.Instance.TryPossess(this);
    }

    public void Possess()
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

    void Update()
    {
        if (!isPossessed)
            return;
        
        if(Input.GetKeyDown(KeyCode.E))
            Unpossess();
    }

    public void OnQTESuccess()
    {
        Debug.Log("QTE 성공 - 빙의 완료");
        isPossessed = true;
        //PossessionStateManager.Instance.Possess(this);
    }

    public void OnQTEFailure()
    {
        Debug.Log("QTE 실패 - 빙의 취소");
        isPossessed = false;
        //PossessionStateManager.Instance.UnPossess();
    }
}
