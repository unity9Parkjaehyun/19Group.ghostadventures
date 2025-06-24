using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionStateManager : Singleton<PossessionStateManager>
{
    public enum State { Ghost, Possessing }
    public State currentState { get; private set; } = State.Ghost;
    
    private GameObject ghostPlayer;
    private GameObject possessedTarget;

    public void Possess(GameObject ghost, GameObject target)
    {
        ghost.SetActive(false);
        target.SetActive(true);
        currentState = State.Possessing;
    }

    public void UnPossess()
    {
        // if(possessedTarget != null)
        //     possessedTarget.SetActive(false);
        // if(ghostPlayer != null)
        //     ghostPlayer.SetActive(true);
        //
        // currentState = State.Ghost;
        Vector3 spawnOffset = new Vector3(1f, 0f, 0f); // 오른쪽 1만큼 띄워서 복귀
        ghostPlayer.transform.position = possessedTarget.transform.position + spawnOffset;
        ghostPlayer.SetActive(true);
        possessedTarget.SetActive(false);
        currentState = State.Ghost;
    }
}
