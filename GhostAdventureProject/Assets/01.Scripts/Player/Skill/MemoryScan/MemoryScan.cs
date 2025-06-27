﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MemoryScan : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float scan_duration = 4f; //스캔 시간

    [Header("UI References")]
    [SerializeField] private GameObject scanPanel; //스캔 패널
    [SerializeField] private Image scanCircleUI; //스캔 원 UI
    [SerializeField] private Transform playerTransform; //플레이어 위치

    // 내부 상태 변수
    private float scanTime = 0f;
    private bool isScanning = false;
    private bool isNearMemory = false;
    [SerializeField] private GameObject currentScanObject; // 현재 스캔 대상 오브젝트
    [SerializeField] private MemoryFragment currentMemoryFragment;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        // 초기 UI 상태 설정
        scanPanel = UIManager.Instance.scanUI;
        scanCircleUI = UIManager.Instance.scanUI.GetComponentInChildren<Image>();
        scanCircleUI?.gameObject.SetActive(false);

    }

    void Update()
    {
        // 스캔 가능한 상태가 아니거나, 스캔 중이 아닐 때 입력을 받음
        if (isNearMemory && !isScanning && Input.GetKeyDown(KeyCode.E))
        {
            TryStartScan();
        }

        // 스캔이 진행 중일 때 로직 처리
        if (isScanning)
        {
            UpdateScan();
        }
    }

    void LateUpdate()
    {
        // UI가 활성화 되어 있을 때만 플레이어 위치를 따라다니도록 처리
        if (scanCircleUI != null && scanCircleUI.gameObject.activeInHierarchy)
        {
            // 플레이어의 월드 위치를 스크린 위치로 변환하여 UI 위치를 갱신
        
            scanCircleUI.transform.position = mainCamera.WorldToScreenPoint(playerTransform.position) + new Vector3(-40, 50, 0);
        }
    }

    private void TryStartScan()
    {
        // 영혼 에너지가 있는지 확인
        if (SoulEnergySystem.Instance.currentEnergy <= 0 )
        {
            Debug.Log("영혼 에너지가 부족하여 스캔을 시작할 수 없습니다.");
            // 여기에 부족 알림 UI나 사운드를 재생하는 로직
            return;
        }

        StartScan();
    }

    private void StartScan()
    {
        if (!currentMemoryFragment.isScanned)
        {

        isScanning = true;
        scanTime = 0f;

        scanPanel?.SetActive(true);
        if (scanCircleUI != null)
        {
            scanCircleUI.gameObject.SetActive(true);
            scanCircleUI.fillAmount = 0f;
        }

        Time.timeScale = 0.3f; // 슬로우 모션 시작
        SoulEnergySystem.Instance.Consume(1); // 에너지 소모
        Debug.Log("스캔 시작");


        }
    }

    private void UpdateScan()
    {
        // 키를 계속 누르고 있는지 확인
        if (Input.GetKey(KeyCode.X))
        {
            scanTime += Time.unscaledDeltaTime; // Time.timeScale에 영향받지 않는 시간으로 진행
            float scanProgress = Mathf.Clamp01(scanTime / scan_duration);
            scanCircleUI.fillAmount = scanProgress;

            // 스캔 완료 체크
            if (scanTime >= scan_duration)
            {
                CompleteScan();
            }
        }
        // 키를 뗐을 경우 스캔 중단
        else
        {
            CancelScan("스캔이 중단");
        }
    }

    private void CompleteScan()
    {
        Debug.Log("스캔 완료");
        isScanning = false;
        Time.timeScale = 1f; // 시간 흐름을 원래대로 복구

        scanPanel?.SetActive(false);
        scanCircleUI?.gameObject.SetActive(false);

        // 이미 스캔되었는지 확인
        if (currentMemoryFragment != null && !currentMemoryFragment.isScanned)
        {
            currentMemoryFragment.IsScanned();
            // 스캔 완료 후 메모리 오브젝트 파괴
            //Destroy(currentScanObject);
        }
        else
        {
            Debug.Log("이미 스캔된 기억 조각이거나, MemoryFragment 컴포넌트가 없습니다.");
        }


        currentScanObject.GetComponentInChildren<SpriteRenderer>().color = new Color(155/255f,155/255f,155/255f); // 스캔 완료 후 색상 변경
        //// 스캔 대상 초기화
        //currentScanObject = null;
        //currentMemoryFragment = null;

        // 여기에 스캔 완료 후 처리 로직 추가 (예: UI 업데이트, 사운드 재생 등)
        
        //SceneManager.LoadScene($"{currentMemoryFragment.data.CutSceneName}", LoadSceneMode.Additive); // 스캔 완료 후 씬 전환
        //Time.timeScale = 0f; // 시간 흐름을 원래대로 복구
        
    }

    private void CancelScan(string reason)
    {
        Debug.Log(reason);
        isScanning = false;
        Time.timeScale = 1f; // 시간 흐름을 원래대로 복구

        scanPanel?.SetActive(false);
        scanCircleUI?.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = true;
            currentScanObject = collision.gameObject;
            currentMemoryFragment = currentScanObject.GetComponent<MemoryFragment>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = false;

            // 스캔 중에 범위를 벗어났다면 스캔을 취소
            if (isScanning)
            {
                CancelScan("범위를 이탈하여 스캔이 중단되었습니다.");
            }

            
            currentScanObject = null;
            currentMemoryFragment = null;
        }
    }
}