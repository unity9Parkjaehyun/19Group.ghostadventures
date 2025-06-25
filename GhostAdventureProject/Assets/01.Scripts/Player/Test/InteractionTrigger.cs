using System.Linq;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"트리거 충돌: {other.gameObject.name} (Tag: {other.tag}) (Trigger: {gameObject.name})");

        if (other.CompareTag("Player"))
        {
            Debug.Log($" 플레이어 진입 감지됨 - Target: {gameObject.name}, Parent: {transform.parent?.name}");
            var playerCtrl = other.GetComponent<PlayerController>();

            // 먼저 현재 GameObject에서 찾고, 없으면 부모에서 찾기
            var target = GetComponent<IInteractionTarget>();
            Debug.Log($"현재 오브젝트에서 Target 찾기: {target != null}");

            if (target == null)
            {
                target = GetComponentInParent<IInteractionTarget>();
                Debug.Log($"부모에서 Target 찾기: {target != null}");
                if (target != null)
                    Debug.Log($"부모 Target 이름: {((MonoBehaviour)target).name}");
            }

            if (playerCtrl != null && target != null)
            {
                Debug.Log($" 상호작용 타겟 설정 성공: {((MonoBehaviour)target).name}");
                playerCtrl.SetInteractTarget(target);
            }
            else
            {
                Debug.Log($" 컴포넌트 없음 - PlayerCtrl: {playerCtrl != null}, Target: {target != null}");
                if (transform.parent != null)
                {
                    Debug.Log($"부모 오브젝트 컴포넌트들: {string.Join(", ", transform.parent.GetComponents<Component>().Select(c => c.GetType().Name))}");
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"플레이어 퇴장 감지됨 - Target: {gameObject.name}");
            var playerCtrl = other.GetComponent<PlayerController>();

            // Exit에서도 부모에서 찾기
            var target = GetComponent<IInteractionTarget>();
            if (target == null)
                target = GetComponentInParent<IInteractionTarget>();

            if (playerCtrl != null && target != null)
                playerCtrl.ClearInteractionTarget(target);
        }
    }
}