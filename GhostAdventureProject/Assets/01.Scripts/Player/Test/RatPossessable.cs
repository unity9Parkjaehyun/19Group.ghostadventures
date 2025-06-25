using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPossessable : AnimalPossessable
{
    public override void OnQTESuccess()  // override 키워드 추가!
    {
        Debug.Log("쥐 빙의 성공");
        isPossessed = true;  // 이것도 추가 (NPC1처럼)
        PossessionStateManager.Instance.Possess(GameManager.Instance.Player, this.gameObject);
    }
}
