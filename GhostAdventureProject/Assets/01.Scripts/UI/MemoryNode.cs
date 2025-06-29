using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MemoryNode : MonoBehaviour
{
    // 컷씬 이미지를 가져와야 하는데 어디서 가져와야함?

    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image glowImage;

    private MemoryData memory;

    public void Init(MemoryData data)
    {
        memory = data;
        iconImage.sprite = memory.MemoryObjectSprite;
        nameText.text = memory.memoryTitle;
    }

    public void OnClick()
    {
        // 클릭 효과 예시
        glowImage.color = Color.white; // 반짝임
        // MemoryDetailUI.Instance.Show(memory);
    }
}
