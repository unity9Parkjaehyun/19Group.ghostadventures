using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    public MemoryData data;
    public bool isScanned = false;

    [Header("드랍 조각 프리팹")]
    [SerializeField] private GameObject fragmentDropPrefab;

    [Header("드랍 시스템")]
    [SerializeField] private Vector2 dropDirection = new Vector2(-1, -1); // 사선 아래로
    [SerializeField] private float dropForce = 2f;
    [SerializeField] private Vector3 dropOffset = new Vector3(0f, 0f, 0f); // 필요 시 조정

    public void IsScanned()
    {
        if (isScanned) return;
        isScanned = true;

        //ShowMemoryImage(data.memoryImage);
        Sprite dropSprite = GetFragmentSpriteByType(data.type);

        // 조각 생성
        if (fragmentDropPrefab != null && dropSprite != null)
        {
            GameObject drop = Instantiate(fragmentDropPrefab, transform.position + dropOffset, Quaternion.identity);

            // 스프라이트 적용
            if (drop.TryGetComponent(out SpriteRenderer sr))
            {
                sr.sprite = dropSprite;
            }

            // 튕겨나오는 연출
            if (drop.TryGetComponent(out Rigidbody2D rb))
            {
                rb.AddForce(dropDirection.normalized * dropForce, ForceMode2D.Impulse);
            }
        }

        //'기억을 재생합니다' 같은 UI
        //UIManager.Instance.ShowMemoryPrompt(this);
    }

    private Sprite GetFragmentSpriteByType(MemoryData.MemoryType type)
    {
        switch (type)
        {
            case MemoryData.MemoryType.Positive:
                return data.PositiveFragmentSprite;
            case MemoryData.MemoryType.Negative:
                return data.NegativeFragmentSprite;
            case MemoryData.MemoryType.Fake:
                return data.FakeFragmentSprite;
            default:
                return null;
        }
    }

    private void ApplyMemoryEffect()
    {
        switch (data.type)
        {
            case MemoryData.MemoryType.Positive:
                //RecoverSoul(data.soulRecovery); // 2만큼 회복
                //영혼에너지 회복 메시지
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
