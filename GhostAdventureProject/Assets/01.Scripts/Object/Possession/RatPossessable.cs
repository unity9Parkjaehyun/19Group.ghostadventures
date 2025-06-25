using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPossessable : AnimalPossessable
{
    public void OnActivate() // qte 성공 시 빙의 전환 요청
    {
        // isPossessed = true;
        PossessionStateManager.Instance.StartPossessionTransition(GameManager.Instance.Player, this.gameObject);
    }
}
