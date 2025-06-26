using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    // 인벤토리 = 단서수집함
    // <완료>
    // 위치 - 화면 하단 좌측

    // 단서 습득시 인벤토리에 저장됨
    // 인벤토리 키를 누르면 단서를 다시 볼 수 있음.


    // 위치2 - 플레이어가 잡고 편한곳으로 옮길 수 있음
    

    public GameObject clueSlotPrefab;
    public Transform clueSlotParent;

    public List<ClueData> collectedClues = new List<ClueData>();

    public void AddClue(ClueData clue)
    {
        
        if (!collectedClues.Contains(clue))
        {
            collectedClues.Add(clue);
            // UI 갱신 이벤트 호출
        }
    }

    public void RefreshUI()
    {
        foreach (Transform child in clueSlotParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var clue in collectedClues)
        {
            GameObject slot = Instantiate(clueSlotPrefab, clueSlotParent);
            slot.GetComponent<InventorySlot>().Setup(clue);
        }
    }
}



