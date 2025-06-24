using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public MemoryData data;
    private bool isScanned = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null && data != null)
        {
            spriteRenderer.sprite = data.unRevealedSprite;
        }
    }

    public void Scan()
    {
        if (isScanned) return;
        isScanned = true;

        //ShowMemoryImage(data.memoryImage);

        switch (data.type)
        {
            case MemoryData.MemoryType.Positive:
                //RecoverSoul(data.soulRecovery); // 3만큼 회복
                break;

            case MemoryData.MemoryType.Negative:
                //ApplyDebuff(); // 디버프는 추후에 추적, 사신 시야 증가 등
                break;

            case MemoryData.MemoryType.Fake:
                FakeEndingManager.Instance.CollectFakeMemory(data.memoryID);
                break;
        }
    }
}
