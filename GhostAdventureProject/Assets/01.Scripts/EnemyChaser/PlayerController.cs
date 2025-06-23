using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 이동,점프높이 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    [Header("숨기 키 설정 ")]
    public KeyCode hideKey = KeyCode.E;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isHiding = false; // 숨어있는지 여부
    private bool canHide = false; // 숨을 수 있는지 여부

    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    public bool IsHiding // isHiding 변수 아래쪽에 추가하여, 외부에서 확인할 수 있도록 함
    {
        get { return isHiding; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    void HidePlayer() // 숨어있다는것을 정의함
    {
        isHiding = true;
        spriteRenderer.enabled = false;
        col.enabled = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true; // 물리 영향도 제거
    }

    void ShowPlayer() // 숨어있지 않다는것을[나왔다는것] 을 정의함
    {
        isHiding = false;
        spriteRenderer.enabled = true;
        col.enabled = true;
        rb.isKinematic = false;
    }

    void Update()
    {
        // 숨은 상태일 때는 이동 금지하고 나올 수만 있게
        if (isHiding)
        {
            if (Input.GetKeyDown(hideKey))
            {
                ShowPlayer();
            }
            return; // 아래 실행 안 함
        }


        // 이동 구현
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

        // 숨기 
        if (Input.GetKeyDown(hideKey) && canHide) // hideKey를 눌렀고 , HideArea에 있다면
        {
            HidePlayer();
        }
    }


    void OnCollisionEnter2D(Collision2D collision) // collision 은 "충돌"
    {
        // Ground 태그가 있는 오브젝트와 충돌했을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Ground에서 떨어졌을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other) // Trigger 는 "감지만" 하고 통과 
    {
        if (other.CompareTag("HideArea")) // HideArea에 들어갔을 때
        {
            canHide = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) // HideArea에서 나갔을 때
    {
        if (other.CompareTag("HideArea")) // HideArea에서 나갔을 때
        {
            canHide = false;
        }
    }

}

