using UnityEngine;
using DG.Tweening;

public class AbsorbObject : MonoBehaviour
{
    public GameObject text;
    public Transform player;
    private bool isPlayerInTrigger = false;
    private bool isAbsorbing = false;
    public Animator animator;
    public GameObject playerObject;
    public GameObject topdia;
    public GameObject chr;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            text.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F) && !isAbsorbing)
        {
            isAbsorbing = true;
            text.SetActive(false);

            transform.DOMove(player.position, 1.5f);
            transform.DOScale(Vector3.zero, 1.5f)
                     .OnComplete(() => Destroy(gameObject));

            animator.SetTrigger("trig");
            topdia.SetActive(true);
            chr.SetActive(true);
            playerObject.SetActive(false);
        }
    }
}
