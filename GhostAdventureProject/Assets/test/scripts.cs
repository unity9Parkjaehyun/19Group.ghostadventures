using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 설정

    void Update()
    {
        // 키보드 입력 받기
        float horizontal = Input.GetAxis("Horizontal"); // A(-1) ↔ D(+1)
        float vertical = Input.GetAxis("Vertical");     // S(-1) ↔ W(+1)

        // 이동 방향 벡터 계산
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        // 이동 실행
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
