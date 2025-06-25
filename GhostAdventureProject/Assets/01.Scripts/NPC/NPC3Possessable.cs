public class NPC3Possessable : BasePossessable
{
    public override void OnQTESuccess()
    {
        isPossessed = true; // ← 이거 꼭 필요!
        PossessionStateManager.Instance.Possess(GameManager.Instance.Player, this.gameObject);
    }

    public override void Unpossess()
    {
        base.Unpossess();
        isPossessed = false; // 해제 시 false
    }
}
