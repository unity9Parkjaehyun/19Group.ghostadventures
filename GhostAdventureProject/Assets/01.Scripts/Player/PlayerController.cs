using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, v, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        Debug.Log(move);
        Debug.Log(h);
        Debug.Log(v);



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

    }
}
