using UnityEngine;

public class NPC1Movement : MonoBehaviour
{
    // isPossessed 변수 삭제! BasePossessable의 것을 사용할 예정
    public float patrolRange = 2f;
    public float speed = 2f;
    public float waitTime = 2f; // 멈추는 시간

    private Vector3 startPoint;
    private int dir = 1;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private BasePossessable possessable; // 추가!

    void Start()
    {
        startPoint = transform.position;
        possessable = GetComponent<BasePossessable>(); // NPC1Possessable 가져오기

    }

    void Update()
    {
        // BasePossessable의 isPossessed 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 입력으로 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(h, 0, 0) * speed * 1.5f * Time.deltaTime;
            transform.position += move;

            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);

            Debug.Log($"NPC1 플레이어 조작 중: {h}"); // 디버그 로그 추가
        }
        else
        {
            // 자동 순찰 모드
            if (isWaiting)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    isWaiting = false;
                    waitTimer = 0f;
                    dir *= -1; // 방향 전환 후 이동 시작
                }
                return;
            }

            transform.position += Vector3.right * dir * speed * Time.deltaTime;

            if (Mathf.Abs(transform.position.x - startPoint.x) > patrolRange)
            {
                // 끝에 도달 시 대기 시작
                isWaiting = true;
                waitTimer = 0f;
                // 도달 시 정확히 patrolRange까지만 이동시킴 (튀는 현상 방지)
                float clampedX = startPoint.x + Mathf.Clamp((transform.position.x - startPoint.x), -patrolRange, patrolRange);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }
        }
    }

}