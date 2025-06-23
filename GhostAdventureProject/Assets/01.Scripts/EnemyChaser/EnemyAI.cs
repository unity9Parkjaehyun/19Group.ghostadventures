using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;  // 따라갈 대상
    public float detectiongRange = 5f; // 감지 범위
    public float moveSpeed = 2f; // 이동 속도

    private Vector3 startPos; // 시작 위치
    private PlayerHide playerHide; // PlayerHide 참조 추가

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // 시작 위치 저장

        // PlayerController 컴포넌트 가져오기
        if (Player != null)
        {
            playerHide = Player.GetComponent<PlayerHide>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 없거나 숨어있다면
        if (Player == null || IsPlayerHiding())
        {
            MoveTo(startPos);
            return;  // 플레이어가 숨었거나 없으면 → 원래 자리로 이동
        }

        float distance = Vector3.Distance(transform.position, Player.position);

        // 감지 범위 안에 있으면 추적, 아니면 원래 자리로
        if (distance < detectiongRange)
        {
            MoveTo(Player.position); // 플레이어 쫓아가기
        }
        else
        {
            MoveTo(startPos); // 원래 자리로 돌아가기
        }
    }

    void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    // 플레이어가 숨어있는지 확인하는 메서드
    private bool IsPlayerHiding()
    {
        if (playerHide == null) return false;
        return playerHide.IsHiding;
    }


}