using Unity.Mathematics;
using UnityEngine;

public class QTERotatingBar : MonoBehaviour // 수정 필요 =======================================================

{
    public RectTransform needle;       // 회전하는 바늘
    public float rotateSpeed = 90f;   // 초당 회전 속도 (도)
    // public float minSuccessAngle = 120f; // 성공 범위 시작 (랜덤값)
    // public float maxSuccessAngle = 150f; // 성공 범위 끝 (랜덤값)
    public KeyCode inputKey = KeyCode.Space;

    private float currentAngle = 0f; // ***** 바늘 움직임이 없을 때 0도, 중앙 -90도, 최대로 갔을 때 -180도. *****
    // [SerializeField] private bool isQTEActive = false;


    void Start()
    {
    }

    public void StartQTE(float minAngle, float maxAngle)
    {
        // gameObject.SetActive(true); // QTE UI 화면에 뜸
        // currentAngle = 0f;
        needle.localEulerAngles = Vector3.zero; // 바늘은 0도부터 시작

        // if (!isQTEActive) return;

        // 바늘 회전
        currentAngle += rotateSpeed * Time.deltaTime;
        if (currentAngle >= 180f) // 반원 한 바퀴 돌면 실패 처리
        {
            // isQTEActive = false;
            Debug.Log("❌ QTE 실패");
            return;
        }

        // 바늘 회전 적용
        needle.localEulerAngles = new Vector3(0, 0, -currentAngle); // 시계방향 회전

        // 입력 감지
        if (Input.GetKeyDown(inputKey))
        {
            if (currentAngle >= minAngle && currentAngle <= maxAngle)
            {
                Debug.Log("✅ QTE 성공!");
            }
            else
            {
                Debug.Log("❌ QTE 실패 (타이밍 안 맞음)");
            }

        }

    }

    void Update()
    {
        StartQTE(90, 110);
    }
}
