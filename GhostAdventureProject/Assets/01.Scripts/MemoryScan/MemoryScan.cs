using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.UI;

public class MemoryScan : MonoBehaviour
{
    [SerializeField] private float scan_duration = 4f;
    private float scanTime = 0f;
    private bool isScanning = false;
    private bool isNearMemory = false; // Memory 오브젝트 근처인지 확인

    private MemoryFragment memoryFragment;



    [SerializeField] private Image scanCircleUI;


    [SerializeField] private Transform playerTransform;

    private Camera mainCamera;
    [SerializeField] private GameObject scanobj;

    void Start()
    {
        mainCamera = Camera.main;

        if (scanCircleUI != null)
        {
            scanCircleUI.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        HandleScanInput();
    }

    void LateUpdate()
    {
        // UI가 활성화 되어 있을 때만 위치를 업데이트
        if (scanCircleUI != null && scanCircleUI.gameObject.activeInHierarchy)
        {
            // 플레이어의 월드 위치를 스크린 위치로 변환하여 UI 위치를 갱신
            scanCircleUI.transform.position = mainCamera.WorldToScreenPoint(playerTransform.position)+  new Vector3 (-40,50,0); //위치 보정
        }
    }

    private void HandleScanInput()
    {
        if (!isNearMemory) return; // 메모리 오브젝트 근처가 아니면 스캔 금지

        if (Input.GetKeyDown(KeyCode.X) && SoulEnergySystem.Instance.currentEnergy == 0)
        {
            Debug.Log("영혼 에너지가 부족하여 스캔을 시작할 수 없습니다.");


        }

            if (Input.GetKeyDown(KeyCode.X)&& SoulEnergySystem.Instance.currentEnergy>0)
        {
            isScanning = true;
            scanTime = 0f;
            if (scanCircleUI != null)
            {
                scanCircleUI.gameObject.SetActive(true);
                scanCircleUI.fillAmount = 0f;
            }
            Time.timeScale = 0f; // 스캔 중 시간 정지
            Debug.Log("스캔 시작");

            SoulEnergySystem.Instance.Consume(1); // 스캔 시작 시 영혼 에너지 1 소모
        }

        if (isScanning && Input.GetKey(KeyCode.X))
        {
            Time.timeScale = 1f; // 스캔 완료 후 시간 재개
            scanTime += Time.unscaledDeltaTime; // Time.unscaledDeltaTime 사용하여 시간 흐름을 유지
            float scanProgress = Mathf.Clamp01(scanTime / scan_duration);
            if (scanCircleUI != null)
            {
                scanCircleUI.fillAmount = scanProgress;
            }
            Debug.Log($"스캔 ({scanProgress * 100:F1}%)");

            if (scanTime >= scan_duration)
            {
                
                isScanning = false;
                Debug.Log("스캔 -완-");
                memoryFragment = scanobj.GetComponent<MemoryFragment>();

                if(memoryFragment.isScanned != true)
                {

                memoryFragment.IsScanned();

                }
                else
                {

                    Debug.Log("이미 스캔된 기억조각입니다.");
                }

                if (scanCircleUI != null)
                {
                    scanCircleUI.gameObject.SetActive(false);
                }

                Destroy(scanobj); // 스캔 완료 후 메모리 오브젝트 삭제


                //이후 기억조각에 따라 실행 될 코드


            }
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            if (isScanning)
            {
                isScanning = false;
                Debug.Log("스캔이 중단");
                if (scanCircleUI != null)
                {
                    scanCircleUI.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = true;
            scanobj = collision.gameObject;
   
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Memory"))
        {
            isNearMemory = false;
            scanobj = null;
            Time.timeScale = 1f; // 범위 이탈 시 시간 재개
            if (isScanning)
            {
                isScanning = false;
                Debug.Log("범위 이탈");
            }
        }
    }
}
 