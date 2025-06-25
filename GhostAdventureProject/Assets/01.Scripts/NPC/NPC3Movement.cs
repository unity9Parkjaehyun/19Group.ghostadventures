using UnityEngine;

public class NPC3Movement : MonoBehaviour
{
    // isPossessed 변수 삭제! BasePossessable의 것을 사용할 예정
    public float fastSpeed = 5f; // 빠른 이동 속도

    private BasePossessable possessable; // 추가!
    private Vector3[] waypoints = { new Vector3(-5f, 0, 0), new Vector3(0f, 0, 0), new Vector3(8f, 0, 0) };
    private float[] waitTimes = { 2f, 2f, 3f }; // 각 지점에서의 대기 시간

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        possessable = GetComponent<BasePossessable>(); // NPC3Possessable 가져오기

        // 시작 위치를 첫 번째 웨이포인트로 설정
        transform.position = new Vector3(waypoints[0].x, transform.position.y, transform.position.z);
        StartWaiting();
    }

    void Update()
    {
        // BasePossessable의 isPossessed 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v) * fastSpeed * 1.5f * Time.deltaTime;
            transform.position += move;

            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            Debug.Log($"NPC3 플레이어 조작 중: H={h}, V={v}"); // 디버그 로그 추가
        }
        else
        {
            // AI 패턴: -5 → 0 → 8 → -5 순찰
            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTimes[currentWaypointIndex])
                {
                    StartMoving();
                }
            }
            else if (isMoving)
            {
                MoveToNextWaypoint();
            }
        }
    }

    void StartWaiting()
    {
        isWaiting = true;
        isMoving = false;
        waitTimer = 0f;
        Debug.Log($"NPC3 대기 시작 - 위치: {waypoints[currentWaypointIndex].x}, 대기시간: {waitTimes[currentWaypointIndex]}초");
    }

    void StartMoving()
    {
        isWaiting = false;
        isMoving = true;

        // 다음 웨이포인트로 인덱스 이동
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        Debug.Log($"NPC3 이동 시작 - 목표: {waypoints[currentWaypointIndex].x}");
    }

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = new Vector3(waypoints[currentWaypointIndex].x, transform.position.y, transform.position.z);

        // 목표 지점까지의 거리
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < 0.1f)
        {
            // 목표 지점 도달
            transform.position = targetPosition; // 정확한 위치로 보정
            StartWaiting();
        }
        else
        {
            // 빠르게 이동
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * fastSpeed * Time.deltaTime;
        }
    }
}