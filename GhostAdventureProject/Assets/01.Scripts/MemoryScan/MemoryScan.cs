using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryScan : MonoBehaviour
{
    [SerializeField] private float scan_duration = 4f;
    private float scanTime = 0f;
    private bool isScanning = false;
    private bool isNearMemory = false; // Memory 오브젝트 근처인지 확인

    void Update()
    {
        HandleScanInput();
    }

    private void HandleScanInput()
    {
        if (!isNearMemory) return; // 메모리 오브젝트 근처가 아니면 스캔 금지

        if (Input.GetKeyDown(KeyCode.E))
        {
            isScanning = true;
            scanTime = 0f;
            Debug.Log("스캔을 시작");
        }

        if (isScanning && Input.GetKey(KeyCode.E))
        {
            scanTime += Time.deltaTime;
            float scanProgress = Mathf.Clamp01(scanTime / scan_duration);
            Debug.Log($"스캔 ({scanProgress * 100:F1}%)");

            if (scanTime >= scan_duration)
            {
                isScanning = false;
                Debug.Log("스캔 -완-");
                // 스캔 성공 처리
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isScanning)
            {
                isScanning = false;
                Debug.Log("스캔이 중단되었습니다.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = false;
            if (isScanning)
            {
                isScanning = false;
                Debug.Log("범위 이탈");
            }
        }
    }
}
 