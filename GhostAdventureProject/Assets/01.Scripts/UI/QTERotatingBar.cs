using UnityEngine;
using UnityEngine.UI;


public class QTERotatingBar : MonoBehaviour

{
    public RectTransform needle;       // 바늘
    public float rotateSpeed = 90f;   // 초당 회전 속도(도)
    public KeyCode inputKey = KeyCode.Space;
    public Image successZone;
    public float minAngle = 120f; // 추후 매개변수로 변경
    public float maxAngle = 150f; // 추후 매개변수로 변경
    private float currentAngle = 0f; // 현재 각도. (바늘은 시작시 0도, 최대로 갔을 때 -180도 (반원))
    private bool isRunning;
    

    public void StartQTE()
    {
        currentAngle = 0f; // 현재 각도 초기화
        isRunning = true; // Update() 시작
        gameObject.SetActive(true); // QTE UI 화면에 뜸
        needle.localEulerAngles = Vector3.zero; // 바늘 각도 0도
        ShowSuccessZone(); // success zone 표시
    }

    private void EndQTE(bool success)
    {
        isRunning = false; // Update() 종료
        gameObject.SetActive(false); // QTE UI 화면에서 지움

        if (success) 
            Debug.Log("QTE 성공");
        else 
            Debug.Log("QTE 실패");
    }

    void Update()
    {
        if(!isRunning) return;
        
        currentAngle += rotateSpeed * Time.deltaTime; // 현재 각도 증가

        if(currentAngle >= 180f)
        {
            EndQTE(false);
            return;
        }

        needle.localEulerAngles = new Vector3(0, 0, -currentAngle); // 바늘 회전 적용 (시계방향)

        if (Input.GetKeyDown(inputKey))
        {
            if (currentAngle >= minAngle && currentAngle <= maxAngle)
                EndQTE(true);
            else
                EndQTE(false);
        }
    }

    void ShowSuccessZone()
    {
        float fill = (maxAngle - minAngle) / 360f;
        successZone.fillAmount = fill;
        
        // minAngle만큼 회전해서 시작 지점 맞추기
        successZone.rectTransform.localEulerAngles = new Vector3(0, 0, -minAngle);
    }

}
