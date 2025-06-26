using UnityEngine;

public class NPC2Movement : MonoBehaviour
{
    public float areaSize = 3f;
    public float speed = 1.5f;

    private Vector3 targetPoint;
    private float waitTime = 0;
    private BasePossessable possessable;

    void Start()
    {
        PickNewTarget();
        possessable = GetComponent<BasePossessable>();
    }

    void Update()
    {
        // BasePossessable의 IsPossessed 프로퍼티 사용
        if (possessable != null && possessable.IsPossessed)
        {
            // 플레이어 조작 (1.5배 빠르게)
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v) * speed * 1.5f * Time.deltaTime;
            transform.position += move;

            // 이동 방향에 따라 스프라이트 뒤집기
            if (h > 0.01f)
                transform.localScale = new Vector3(1, 1, 1);
            else if (h < -0.01f)
                transform.localScale = new Vector3(-1, 1, 1);
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

    void PickNewTarget() // 현시점 맵에 따라 pos.x -8 ~8 로 랜덤하게 조정함 나중에 min max 값 바꾸면 됨
    {
        float minX = -8f;
        float maxX = 8f;
        float centerX = Mathf.Clamp(transform.position.x, minX + areaSize, maxX - areaSize);
        float newX = Mathf.Clamp(
            centerX + Random.Range(-areaSize, areaSize),
            minX,
            maxX
        );
        float newZ = transform.position.z + Random.Range(-areaSize, areaSize);
        targetPoint = new Vector3(newX, transform.position.y, newZ);
    }
}