using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionSystem : Singleton<PossessionSystem>
{
    //private PlayerController Player => GameManager.Instance.PlayerController;
    private PlayerController Player;
    private BasePossessable currentTarget;

    public bool isLocked { get; private set; } = false;

    private void Start()
    {
        // 게임매니저 이어지는지 확인
        Player = FindObjectOfType<PlayerController>();
        currentTarget = Player.currentTarget;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"트리거 충돌: {other.name}");
        var possessionObject = other.GetComponent<BasePossessable>();
        if (possessionObject != null)
        {
            SetInteractTarget(possessionObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var possessionObject = other.GetComponent<BasePossessable>();
        if (possessionObject != null)
        {
            ClearInteractionTarget(possessionObject);
        }
    }
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

    public void SetInteractTarget(BasePossessable target)
    {
        currentTarget = target;
        if (Player != null)
            Player.currentTarget = target;
    }

    public void ClearInteractionTarget(BasePossessable target)
    {
        if (currentTarget == target)
        {
            currentTarget = null;
            if (Player != null)
                Player.currentTarget = null;
        }
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
