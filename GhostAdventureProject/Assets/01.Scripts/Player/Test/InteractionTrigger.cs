using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"트리거 충돌: {collision.name}");
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("플레이어 진입 감지됨");
            player.SetInteractTarget(GetComponent<IInteractionTarget>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ClearInteractionTarget(GetComponent<IInteractionTarget>());
        }
    }
}
