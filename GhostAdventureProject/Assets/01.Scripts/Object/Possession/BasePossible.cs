﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePossible : MonoBehaviour
{
    [SerializeField] protected bool isPossessed = false;

    public bool IsPossessedState => isPossessed;
    

    protected virtual void Update()
    {
        if (!isPossessed)
            return;

        if (Input.GetKeyDown(KeyCode.E))
            Unpossess();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerInteractSystem.Instance.AddInteractable(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            PlayerInteractSystem.Instance.RemoveInteractable(gameObject);
    }

    public void Unpossess()
    {
        Debug.Log("빙의 해제");
        isPossessed = false;
        PossessionStateManager.Instance.StartUnpossessTransition();
    }


    public void OnQTESuccess()
    {
        Debug.Log("QTE 성공 - 빙의 완료");

        isPossessed = true;
        PossessionStateManager.Instance.StartPossessionTransition();
    }

    public void OnQTEFailure()
    {
        Debug.Log("QTE 실패 - 빙의 취소");
        isPossessed = false;
        SoulEnergySystem.Instance.Consume(1);
    }
}
