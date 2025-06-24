using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MemoryFragment : MonoBehaviour
{
    private MemoryData data;
    public bool isScanned = false;
    public GameObject interactionInfo;

    [Header("드랍 조각 프리팹")]
    [SerializeField] private GameObject fragmentDropPrefab;

    [Header("드랍 연출")]
    [SerializeField] private Vector3 dropOffset = new Vector3(0f, 0f, 0f); // 생성 위치 조정
    [SerializeField] private float bounceHeight = 0.3f;
    [SerializeField] private float bounceDuration = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isScanned)
        {
            interactionInfo.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionInfo.SetActive(false);
        }
    }

    public void IsScanned()
    {
        if (isScanned) return;
        isScanned = true;

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
            StartCoroutine(DropAndPlayMemory(drop));
        }
    }

    private IEnumerator DropAndPlayMemory(GameObject drop)
    {
        yield return StartCoroutine(DropBounceAnimation(drop));
        //yield return StartCoroutine(기억연출);
    }

    private IEnumerator DropBounceAnimation(GameObject drop)
    {
        Vector3 startPos = drop.transform.position;
        Vector3 peakPos = startPos + new Vector3(0, bounceHeight, 0);
        Vector3 endPos = startPos;

        float half = bounceDuration / 2f;
        float t = 0f;

        while (t < half)
        {
            drop.transform.position = Vector3.Lerp(startPos, peakPos, t / half);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        while (t < half)
        {
            drop.transform.position = Vector3.Lerp(peakPos, endPos, t / half);
            t += Time.deltaTime;
            yield return null;
        }

        drop.transform.position = endPos;
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
