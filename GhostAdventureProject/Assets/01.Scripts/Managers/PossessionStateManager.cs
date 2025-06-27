﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionStateManager : Singleton<PossessionStateManager>
{
    private PlayerController playerController;
    public enum State { Ghost, Possessing }
    public State currentState { get; private set; } = State.Ghost;

    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 1f, 0f);

    private Transform PlayerTransform
        => GameManager.Instance.PlayerController.transform;
    private GameObject Player
        => GameManager.Instance.Player;
    private BasePossessable possessedTarget;

    public bool IsPossessing() => currentState == State.Possessing;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void StartPossessionTransition() // 빙의 전환 실행 ( 빙의 애니메이션도 함께 )
    {
        PossessionSystem.Instance.PlayPossessionInAnimation();
    }

    public void PossessionInAnimationComplete() // 빙의 애니메이션 종료 후 빙의 전환 완료 처리
    {
        // 게임매니저 연결 수정
        playerController.gameObject.SetActive(false);
        PossessionSystem.Instance.canMove = true;
        /// 추가적인 연출이나 효과
        /// 빙의오브젝트 강조효과, 사운드 등
        currentState = State.Possessing;
    }

    public void StartUnpossessTransition() // 빙의 해체 요청 ( 위치 이동 , 활성화, 빙의 해제 애니메이션 실행 )
    {
        // 게임매니저 연결 후 수정
        playerController.transform.position = possessedTarget.transform.position + spawnOffset;
        playerController.gameObject.SetActive(true);
        PossessionSystem.Instance.StartPossessionOutSequence();
    }
    
    public void PossessionOutAnimationComplete() // 빙의 해제 애니메이션 종료 후 상태 복귀
    {
        currentState = State.Ghost;
    }
}
