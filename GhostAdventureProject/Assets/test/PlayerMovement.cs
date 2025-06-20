using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject targetObjectToHide; // 줌 후 꺼질 오브젝트
    public GameObject vet; // 새로 따라갈 대상

    private Vector2 moveDirection;
    private bool isZooming = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(h, v).normalized;

        if (h > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // 스페이스바 = 줌인 + 오브젝트 끄기
        if (Input.GetKeyDown(KeyCode.Space) && !isZooming)
        {
            StartCoroutine(ZoomAndDeactivate(2.5f, 1.55f, 0.5f));
        }

        // F키 = 추적 대상을 vet으로 변경하고 줌아웃
        if (Input.GetKeyDown(KeyCode.F) && vet != null && !isZooming)
        {
            virtualCamera.Follow = vet.transform;
            StartCoroutine(ZoomTo(virtualCamera.m_Lens.OrthographicSize, 3.44f, 1f)); // 줌아웃
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    // 카메라 줌 + 오브젝트 비활성화
    System.Collections.IEnumerator ZoomAndDeactivate(float fromSize, float toSize, float duration)
    {
        isZooming = true;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(fromSize, toSize, t);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = toSize;

        if (targetObjectToHide != null)
            targetObjectToHide.SetActive(false);

        isZooming = false;
    }

    // 일반적인 줌 (줌인 or 줌아웃)
    System.Collections.IEnumerator ZoomTo(float fromSize, float toSize, float duration)
    {
        isZooming = true;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(fromSize, toSize, t);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = toSize;
        isZooming = false;
    }
}
