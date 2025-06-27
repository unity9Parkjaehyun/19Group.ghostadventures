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

    private MemoryData memory;

    public void Init(MemoryData data)
    {
        memory = data;
    //     iconImage.sprite = data.memoryIcon;
        nameText.text = data.memoryTitle;
    }

    public void OnClick()
    {
        // 상세 보기 창 열기
        MemoryDetailUI.Instance.Show(memory);
    }
}
