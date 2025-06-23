using UnityEngine;

[CreateAssetMenu(menuName = "Memory/MemoryData")]
public class MemoryData : ScriptableObject
{
    public enum MemoryType { Positive, Negative, Fake }

    public string memoryID;
    public MemoryType type;

    public Sprite disguisedSprite;   // 스캔 전 외형
    public Sprite revealedSprite;    // 스캔 후 외형
    public Sprite memoryImage;       // 연출용 이미지
    public string memoryTitle;

    [TextArea(3, 10)]
    public string memoryDescription;
}