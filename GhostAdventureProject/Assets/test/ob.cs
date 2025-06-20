using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class AbsorbObject : MonoBehaviour
{
    public Transform player;
    public PostProcessVolume volume; // 여기에는 진짜 볼륨 컴포넌트 연결함 (드래그 가능)
    private ColorGrading colorGrading; // 여기에 saturation 설정을 꺼내서 조작할 거야

    private bool isPlayerInTrigger = false;
    private bool isAbsorbing = false;
    public int count = 0;

    private void Start()
    {
        // ColorGrading 설정을 볼륨에서 가져오기
        if (volume.profile.TryGetSettings(out colorGrading))
        {
            Debug.Log("ColorGrading 가져오기 성공");
        }
        else
        {
            Debug.LogError("ColorGrading이 PostProcessProfile에 추가되어 있는지 확인해줘!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F) && !isAbsorbing)
        {
            isAbsorbing = true;

            transform.DOMove(player.position, 1.5f);
            transform.DOScale(Vector3.zero, 1.5f)
                     .OnComplete(() => Destroy(gameObject));

            count++;
        }

        if (count == 2)
        {
            count++;
            FadeToBlackAndWhite();
        }
    }

    void FadeToBlackAndWhite()
    {
        if (colorGrading != null)
        {
            DOTween.To(
                () => colorGrading.saturation.value,
                x => colorGrading.saturation.value = x,
                -100f,
                3f
            );
        }
    }
}
