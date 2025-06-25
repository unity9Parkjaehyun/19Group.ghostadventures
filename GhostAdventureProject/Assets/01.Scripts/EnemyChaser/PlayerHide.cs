using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public KeyCode hideKey = KeyCode.F;


    private bool isHiding = false;
    private bool canHide = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    private Rigidbody2D rb;

    public bool IsHiding => isHiding;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isHiding)
        {
            if (Input.GetKeyDown(hideKey))
            {
                ShowPlayer();
            }
            return;
        }

        if (Input.GetKeyDown(hideKey) && canHide)
        {
            HidePlayer();
        }
    }

    private void HidePlayer()
    {
        isHiding = true;
        spriteRenderer.enabled = false;
        col.enabled = false;
        rb.velocity = Vector2.zero;     // 멈추기
        rb.isKinematic = true;
    }

    private void ShowPlayer()
    {
        isHiding = false;
        spriteRenderer.enabled = true;
        col.enabled = true;
        rb.isKinematic = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HideArea"))
        {
            canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HideArea"))
        {
            canHide = false;
        }
    }
}
