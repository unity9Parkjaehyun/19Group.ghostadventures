using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionStateManager : Singleton<PossessionStateManager>
{
    public enum State { Ghost, Possessing }
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1f, 0f);
    
    public State currentState { get; private set; } = State.Ghost;
    
    private GameObject ghostPlayer;
    private GameObject possessedTarget;
    public bool IsPossessing() => currentState == State.Possessing;
    
    public void StartPossessionTransition(GameObject ghost, GameObject target) // 빙의 전환 실행 ( 빙의 애니메이션도 함께 )
    {
        ghostPlayer = ghost;
        possessedTarget = target;
        ghostPlayer.GetComponent<PlayerController>().PlayPossessionInAnimation();
    }

    public void PossessionInAnimationComplete() // 빙의 애니메이션 종로 후 빙의 전환 완료 처리
    {
        ghostPlayer.SetActive(false);
        possessedTarget.SetActive(true);
        currentState = State.Possessing;
        if (possessedTarget.TryGetComponent(out BasePossessable possessable))
            possessable.SetPossessed(true);
    }

    public void StartUnpossessTransition() // 빙의 해체 요청 ( 위치 이동 , 활성화, 빙의 해제 애니메이션 실행 )
    {
        // possessedTarget.SetActive(false);
        ghostPlayer.transform.position = possessedTarget.transform.position + spawnOffset;
        ghostPlayer.SetActive(true);
        ghostPlayer.GetComponent<PlayerController>().StartPossessionOutSequence();
    }
    
    public void PossessionOutAnimationComplete() // 빙의 해제 애니메이션 종료 후 상태 복귀
    {
        currentState = State.Ghost;
    }
}
