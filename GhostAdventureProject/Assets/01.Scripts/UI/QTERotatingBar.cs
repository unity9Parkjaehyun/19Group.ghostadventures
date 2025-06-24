using UnityEngine;
using UnityEngine.UI;


public class QTERotatingBar : MonoBehaviour

{
    public RectTransform needle;       // 바늘
    public float rotateSpeed = 90f;   // 초당 회전 속도(도)
    public KeyCode inputKey = KeyCode.Space;
    public Image successArc;
    public float minAngle = 120f; // 추후 매개변수로 변경
    public float maxAngle = 150f; // 추후 매개변수로 변경
    private float currentAngle = 0f; // 현재 각도. (바늘은 시작시 0도, 최대로 갔을 때 -180도 (반원))
    

    public void StartQTE()
    {
        
        gameObject.SetActive(true); // QTE UI 화면에 뜸
        needle.localEulerAngles = Vector3.zero; // 바늘은 0도부터 시작

        // 바늘 회전
        currentAngle += rotateSpeed * Time.deltaTime;
        if (currentAngle >= 180f) // 반원 한 바퀴 돌면 실패 처리
        {
            Debug.Log("❌ QTE 실패");
            gameObject.SetActive(false);
            currentAngle = 0f;  // 현재각도 초기화
            return;
        }
        
        needle.localEulerAngles = new Vector3(0, 0, -currentAngle); // 바늘 회전 적용 (시계방향)

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
            currentAngle = 0f;  // 현재각도 초기화
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
