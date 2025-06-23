using UnityEngine;
using UnityEngine.UI;


public class QTERotatingBar : MonoBehaviour // 수정 필요 =======================================================

{
    public RectTransform needle;       // 회전하는 바늘
    public float rotateSpeed = 90f;   // 초당 회전 속도 (도)
    public KeyCode inputKey = KeyCode.Space;

    public Image successArc;
    public float minAngle = 120f;
    public float maxAngle = 150f;

    private float currentAngle = 0f; // ***** 바늘 움직임이 없을 때 0도, 중앙 -90도, 최대로 갔을 때 -180도. *****
    // [SerializeField] private bool isQTEActive = false;


    void Start()
    {
    }

    public void StartQTE()
    {
        gameObject.SetActive(true); // QTE UI 화면에 뜸
        // currentAngle = 0f;
        needle.localEulerAngles = Vector3.zero; // 바늘은 0도부터 시작

        // if (!isQTEActive) return;

        // 바늘 회전
        currentAngle += rotateSpeed * Time.deltaTime;
        if (currentAngle >= 180f) // 반원 한 바퀴 돌면 실패 처리
        {
            // isQTEActive = false;
            Debug.Log("❌ QTE 실패");
            gameObject.SetActive(false);
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
            gameObject.SetActive(false);

        }

    }


    void ShowSuccessArc()
    {
        float fill = (maxAngle - minAngle) / 360f;
        successArc.fillAmount = fill;

        // minAngle만큼 회전해서 시작 지점 맞추기
        successArc.rectTransform.localEulerAngles = new Vector3(0, 0, -minAngle);
    }

    void Update()
    {
        StartQTE();
        ShowSuccessArc();
    }
}
