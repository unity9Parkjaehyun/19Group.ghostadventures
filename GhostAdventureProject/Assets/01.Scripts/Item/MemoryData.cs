using UnityEngine;

[CreateAssetMenu(menuName = "Memory/MemoryData")]
public class MemoryData : ScriptableObject
{
    public enum MemoryType { Positive, Negative, Fake }

    public string memoryID;
    public MemoryType type;

    // 스캔 전 외형
    public Sprite unRevealedSprite;
    // 스캔 후 외형
    public Sprite revealedSprite;

    // 갖고 있는 기억은 나중에
    //public Sprite memoryImage;       
    public string memoryTitle;

    [TextArea(3, 10)]
    public string memoryDescription;

    public int soulRecovery = 0;
}