using UnityEngine;

public class NPC2Movement : MonoBehaviour
{
    // isPossessed 변수 삭제! BasePossessable의 것을 사용할 예정
    public float areaSize = 3f;
    public float speed = 1.5f;
    private Vector3 targetPoint;
    private float waitTime = 0;
    private BasePossessable possessable; // 추가!

    void Start()
    {
        PickNewTarget();
        possessable = GetComponent<BasePossessable>(); // NPC2Possessable 가져오기
    }

    void Update()
    {
        // BasePossessable의 isPossessed 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v) * speed * 1.5f * Time.deltaTime;
            transform.position += move;

            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            Debug.Log($"NPC2 플레이어 조작 중: H={h}, V={v}"); // 디버그 로그 추가
        }
        else
        {
            // AI 패턴: 랜덤 포인트로 천천히 이동
            if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
            {
                waitTime += Time.deltaTime;
                if (waitTime > 1f)
                {
                    PickNewTarget();
                    waitTime = 0;
                }
            }
            else
            {
                Vector3 dir = (targetPoint - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
            }
        }
    }

    void PickNewTarget()
    {
        // 현재 Y, Z는 유지하고 X만 제한된 범위에서 선택
        float minX = -8f;
        float maxX = 8f;
        // 현재 위치 중심에서 areaSize 반경 내에서만 이동, 단 전체가 minX~maxX 안에 있어야 함
        float centerX = Mathf.Clamp(transform.position.x, minX + areaSize, maxX - areaSize);
        float newX = Mathf.Clamp(
            centerX + Random.Range(-areaSize, areaSize),
            minX,
            maxX
        );
        // Y, Z는 필요에 따라 제한(여기선 Z만 움직이도록 유지)
        float newZ = transform.position.z + Random.Range(-areaSize, areaSize);
        targetPoint = new Vector3(newX, transform.position.y, newZ);
    }
}