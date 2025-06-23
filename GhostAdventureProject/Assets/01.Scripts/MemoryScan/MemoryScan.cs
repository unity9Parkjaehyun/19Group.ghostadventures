using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryScan : MonoBehaviour
{
    private float scanTime = 0f;
    private bool isScanning = false; 
    [SerializeField] private float scan_duration  = 4f; // 스캔 완료까지 걸리는 시간 
    void Start()
    {
        
    }


    void Update()
    {

        HandleScanInput();
    }

    private void HandleScanInput()
    {
        // E키를  눌렀을 때 스캔 시작
        if (Input.GetKeyDown(KeyCode.E))
        {
            isScanning = true;
            scanTime = 0f; // 시간 초기화
            Debug.Log("스캔을 시작합니다...");
            // 여기에 스캔 시작 시각 효과 
        }

        // E키를 누르고 있는 동안
        if (isScanning && Input.GetKey(KeyCode.E))
        {
         
            scanTime += Time.deltaTime;

            // 0~1 범위로 스캔 진행률 계산
            float scanProgress = Mathf.Clamp01(scanTime / scan_duration);
            Debug.Log($"스캔 ({scanProgress * 100:F1}%)");
            // 여기에 스캔 진행 중 시각 효과


            if (scanTime >= scan_duration)
            {
                Debug.Log("스캔 -완-");
                isScanning = false; 
                // 여기에 스캔 성공 시의 로직 
            }
        }

        // E키에서 손을 뗐을 때
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isScanning) // 스캔이 완료되지 않고 중간에 멈췄을 때
            {
                isScanning = false;
                Debug.Log("스캔이 중단되었습니다.");
                // 여기에 스캔 취소 효과
            }
        }
    }
}
