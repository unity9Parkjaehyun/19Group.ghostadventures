using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionStateManager : Singleton<PossessionStateManager>
{
    public enum State { Ghost, Possessing }
    [SerializeField] private Vector3 spawnOffset = new Vector3(1f, 0f, 0f);
    
    public State currentState { get; private set; } = State.Ghost;
    
    private GameObject ghostPlayer;
    private GameObject possessedTarget;

    public void Possess(GameObject ghost, GameObject target)
    {
        ghostPlayer = ghost;
        possessedTarget = target;

        ghostPlayer.SetActive(false);
        // possessedTarget.SetActive(true);
        currentState = State.Possessing;
    }

    public void UnPossess()
    {
        if (ghostPlayer != null && possessedTarget != null)
        {
            ghostPlayer.transform.position = possessedTarget.transform.position + spawnOffset;
            ghostPlayer.SetActive(true);
            possessedTarget.SetActive(true);
        }
        currentState = State.Ghost;
    }
}
