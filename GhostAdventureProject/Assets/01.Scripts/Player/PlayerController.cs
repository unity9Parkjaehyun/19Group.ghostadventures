using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private BasePossessable currentTarget;
    
    public Animator animator { get; private set; }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PossessionSystem.Instance.isLocked || PossessionQTESystem.Instance.isRunning)
            return;

        HandleMovement();

        if (Input.GetKeyDown(KeyCode.E) && currentTarget != null)
            currentTarget.OnTryPossess();
    }

    private void HandleMovement() // 기본 이동 처리
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, v, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        
        // 회전
        if (h > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (h < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        
        bool isMoving = move.magnitude > 0.01f;
        animator.SetBool("Move", isMoving);
    }

    //private void HandleInteraction() // 상호작용 입력 처리
    //{
    //    if(Input.GetKeyDown(KeyCode.E) && currentTarget != null)
    //        currentTarget.Interact();
    //}

    //public void SetInteractTarget(BasePossessable target) // 플레이어가 대상 가까이 갈때마다 트리거에서 호출 추천
    //{
    //    currentTarget = target;
    //}

    //public void ClearInteractionTarget(BasePossessable target)
    //{
    //    if (currentTarget == null)
    //        currentTarget = null;
    //}
    
    //public void PlayPossessionInAnimation() // 빙의 시작 애니메이션
    //{
    //    isLocked = true;
    //    animator.SetTrigger("PossessIn");
    //}
  
    //public void StartPossessionOutSequence() // 빙의 해제 애니메이션 코루틴으로
    //{
    //    StartCoroutine(DelayedPossessionOutPlay());
    //}

    //private IEnumerator DelayedPossessionOutPlay()
    //{
    //    yield return null; // 한 프레임 딜레이
    //    isLocked = true;
    //    animator.Play("Player_PossessionOut");
    //}

    //public void OnPossessionInAnimationComplete() // 빙의 시작 애니메이션 후 이벤트
    //{
    //    isLocked = false;
    //    PossessionStateManager.Instance.PossessionInAnimationComplete();
    //}

    //public void OnPossessionOutAnimationComplete() // 빙의 해제 애니메이션 후 이벤트
    //{
    //    isLocked = false;
    //    PossessionStateManager.Instance.PossessionOutAnimationComplete();
    //}
}
