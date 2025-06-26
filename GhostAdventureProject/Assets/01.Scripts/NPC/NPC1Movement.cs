using UnityEngine;

public class NPC1Movement : MonoBehaviour
{
    public float patrolRange = 2f;
    public float speed = 2f;
    public float waitTime = 2f;

    private Vector3 startPoint;
    private int dir = 1;
    private bool isWaiting = false;
    private float waitTimer = 0f;
    private BasePossessable possessable;

    void Start()
    {
        startPoint = transform.position;
        possessable = GetComponent<BasePossessable>();
    }

    void Update()
    {
        // BasePossessable의 IsPossessed 프로퍼티 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 입력으로 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(h, 0, 0) * speed * 1.5f * Time.deltaTime;
            transform.position += move;

            // 이동 방향에 따라 스프라이트 뒤집기
            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
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
                    dir *= -1;
                }
                return;
            }

            transform.position += Vector3.right * dir * speed * Time.deltaTime;

            if (Mathf.Abs(transform.position.x - startPoint.x) > patrolRange)
            {
                isWaiting = true;
                waitTimer = 0f;
                float clampedX = startPoint.x + Mathf.Clamp((transform.position.x - startPoint.x), -patrolRange, patrolRange);
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }
        }
    }
}