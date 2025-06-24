using System.Collections.Generic;
using UnityEngine;

public class FakeEndingManager : Singleton<FakeEndingManager>
{
    private HashSet<string> collectedFakeMemories = new();

    public void CollectFakeMemory(string id)
    {
        collectedFakeMemories.Add(id);
        Debug.Log($"가짜 기억 수집: {id}");

        if (AllFakeMemoriesCollected())
        {
            UnlockFakeEnding();
        }
    }

    // 전체 다 수집했는지 수량 확인
    private bool AllFakeMemoriesCollected()
    {
        return collectedFakeMemories.Count >= 5;
    }

    private void UnlockFakeEnding()
    {
        Debug.Log("진엔딩 해금!");
        // 트리거 설정, 플래그 저장 등
    }
}
