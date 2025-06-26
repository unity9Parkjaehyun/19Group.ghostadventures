using UnityEngine;

public class NPC3Movement : MonoBehaviour
{
    public float fastSpeed = 5f;

    private BasePossessable possessable;
    private Vector3[] waypoints = { new Vector3(-5f, 0, 0), new Vector3(0f, 0, 0), new Vector3(8f, 0, 0) };
    private float[] waitTimes = { 2f, 2f, 3f };

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        possessable = GetComponent<BasePossessable>();
        transform.position = new Vector3(waypoints[0].x, transform.position.y, transform.position.z);
        StartWaiting();
    }

    void Update()
    {
        // BasePossessable의 IsPossessed 프로퍼티 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v) * fastSpeed * 1.5f * Time.deltaTime;
            transform.position += move;

            // 이동 방향에 따라 스프라이트 뒤집기
            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
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
    }

    void StartMoving()
    {
        isWaiting = false;
        isMoving = true;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = new Vector3(waypoints[currentWaypointIndex].x, transform.position.y, transform.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < 0.1f)
        {
            transform.position = targetPosition;
            StartWaiting();
        }
        else
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * fastSpeed * Time.deltaTime;
        }
    }
}