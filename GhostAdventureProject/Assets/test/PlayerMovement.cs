using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(h, v).normalized;

        animator.SetFloat("Speed", moveDirection.magnitude);

        if (h > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); 
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
