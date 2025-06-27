using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup_test : MonoBehaviour
{
    [Header("단서 ScriptableObject를 넣어주세요")]
    public ClueData clueData;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))  // 키입력 조건 추가예정
        {
            Inventory.Instance.AddClue(clueData);
            Destroy(gameObject); // 단서 오브젝트 제거
            Inventory.Instance.RefreshUI(); // UI에 반영
        }
    }
}
