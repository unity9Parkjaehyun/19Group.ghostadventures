using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePickup_test : MonoBehaviour
{
    public ClueData clueData;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Inventory.Instance.AddClue(clueData);
            // 효과음, 이펙트 등
            Destroy(gameObject); // 단서 오브젝트 제거
            Inventory.Instance.RefreshUI();
        }
    }
}
