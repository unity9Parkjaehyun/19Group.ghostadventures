using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionSystem : Singleton<PossessionSystem>
{
    private PlayerController Player => GameManager.Instance.PlayerController;
    private BasePossessable currentTarget;

    public bool isLocked { get; private set; } = false;

    public bool TryPossess(BasePossessable target)
    {
        if (!SoulEnergySystem.Instance.HasEnoughEnergy(3))
        {
            Debug.Log("Not enough energy");
            return false;
        }
        SoulEnergySystem.Instance.Consume(3);
        target.RequestPossession();
        return true;
    }

    public void SetInteractTarget(BasePossessable target) // 플레이어가 대상 가까이 갈때마다 트리거에서 호출 추천
    {
        currentTarget = target;
    }

    public void ClearInteractionTarget(BasePossessable target)
    {
        if (currentTarget == null)
            currentTarget = null;
    }

    public void PlayPossessionInAnimation() // 빙의 시작 애니메이션
    {
        isLocked = true;
        Player.animator.SetTrigger("PossessIn");
    }

    public void StartPossessionOutSequence() // 빙의 해제 애니메이션 코루틴으로
    {
        StartCoroutine(DelayedPossessionOutPlay());
    }

    private IEnumerator DelayedPossessionOutPlay()
    {
        yield return null; // 한 프레임 딜레이
        isLocked = true;
        Player.animator.Play("Player_PossessionOut");
    }

    public void OnPossessionInAnimationComplete() // 빙의 시작 애니메이션 후 이벤트
    {
        isLocked = false;
        PossessionStateManager.Instance.PossessionInAnimationComplete();
    }

    public void OnPossessionOutAnimationComplete() // 빙의 해제 애니메이션 후 이벤트
    {
        isLocked = false;
        PossessionStateManager.Instance.PossessionOutAnimationComplete();
    }
}
