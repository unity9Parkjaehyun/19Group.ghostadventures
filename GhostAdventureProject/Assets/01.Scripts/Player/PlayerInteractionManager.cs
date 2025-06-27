using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionManager : Singleton<PlayerInteractionManager>
{
    private List<GameObject> nearbyInteractables = new();
    private GameObject currentClosest;

    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        if (nearbyInteractables.Count == 0)
        {
            UpdateClosest(null);
            return;
        }

        GameObject closest = null;
        float closestDist = float.MaxValue;

        // 가장 가까운 오브젝트 판별
        foreach (var obj in nearbyInteractables)
        {
            if (obj == null) continue;
            float dist = Vector3.Distance(playerTransform.position, obj.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = obj;
            }
        }

        UpdateClosest(closest);
    }

    private void UpdateClosest(GameObject newClosest)
    {
        if (currentClosest == newClosest) return;

        // 이전 상호작용 대상 숨기기
        if (currentClosest != null)
        {
            currentClosest.GetComponent<BaseInteractable>()?.SetInteractionPopup(false);
        }

        currentClosest = newClosest;

        // 새 상호작용 대상 표시
        if (currentClosest != null)
        {
            currentClosest.GetComponent<BaseInteractable>()?.SetInteractionPopup(true);
        }
    }

    // 플레이어 근처에 있는 오브젝트들
    public void AddInteractable(GameObject obj)
    {
        if (!nearbyInteractables.Contains(obj))
            nearbyInteractables.Add(obj);
    }

    // 플레이어 근처를 벗어난 오브젝트들
    public void RemoveInteractable(GameObject obj)
    {
        if (nearbyInteractables.Contains(obj))
            nearbyInteractables.Remove(obj);
    }
}
