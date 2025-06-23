using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private IInteractionTarget currentTarget;
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
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

    private void HandleInteraction()
    {
        if(Input.GetKeyDown(KeyCode.E) && currentTarget != null)
            currentTarget.Interact();
    }

    public void SetInteractTarget(IInteractionTarget target) // 플레이어가 대상 가까이 갈때마다 트리거에서 호출 추천
    {
        currentTarget = target;
    }

    public void ClearInteractionTarget(IInteractionTarget target)
    {
        if (currentTarget == null)
            currentTarget = null;
    }
}
