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
    
    public void Possess(GameObject ghost, GameObject target)
    {
        ghostPlayer = ghost;
        possessedTarget = target;
        ghostPlayer.GetComponent<PlayerController>().PlayPossessionInAnimation();
    }

    public void PossessionInAnimationComplete()
    {
        ghostPlayer.SetActive(false);
        possessedTarget.SetActive(true);
        currentState = State.Possessing;
        if (possessedTarget.TryGetComponent(out BasePossessable possessable))
            possessable.SetPossessed(true);
    }

    public void UnPossess()
    {
        // possessedTarget.SetActive(false);
        ghostPlayer.transform.position = possessedTarget.transform.position + spawnOffset;
        ghostPlayer.SetActive(true);
        ghostPlayer.GetComponent<PlayerController>().StartPossessionOutSequence();
    }


    public void PossessionOutAnimationComplete()
    {
        currentState = State.Ghost;
    }
}
