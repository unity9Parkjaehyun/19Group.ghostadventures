using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryStorage : MonoBehaviour
{
    public static MemoryStorage Instance;

    [SerializeField] private GameObject memoryStorage;
    [SerializeField] private Transform mapRoot;
    [SerializeField] private GameObject memoryNodePrefab;
    [SerializeField] private GameObject linePrefab;

    private void Awake()
    {
        Instance = this;
        memoryStorage.SetActive(false);
    }

    public void RefreshUI(List<MemoryData> collectedMemories)
    {
        foreach (Transform child in mapRoot)
            Destroy(child.gameObject);

        // 나이순으로 정렬
        // collectedMemories.Sort((a, b) => a.age.CompareTo(b.age));

        Vector2 startPos = new Vector2(0, 0);
        Vector2 offset = new Vector2(300, 0); // 노드 간 거리
        Vector2 curPos = startPos;

        GameObject lastNode = null;

        foreach (var memory in collectedMemories)
        {
            var node = Instantiate(memoryNodePrefab, mapRoot);
            node.GetComponent<RectTransform>().anchoredPosition = curPos;

            var memoryNode = node.GetComponent<MemoryNode>();
            memoryNode.Init(memory);

            // 이전 노드와 선 연결
            if (lastNode != null)
            {
                var line = Instantiate(linePrefab, mapRoot).GetComponent<Image>();
                // 여기서 라인 그리기 (Image로 선 그리거나 LineRenderer)
                DrawLineBetween(lastNode.GetComponent<RectTransform>(), node.GetComponent<RectTransform>(), line);
            }

            lastNode = node;
            curPos += offset;
        }
    }

    private void DrawLineBetween(RectTransform from, RectTransform to, Image line)
    {
        Vector2 dir = to.anchoredPosition - from.anchoredPosition;
        float distance = dir.magnitude;

        line.rectTransform.sizeDelta = new Vector2(distance, 5);
        line.rectTransform.anchoredPosition = from.anchoredPosition + dir / 2f;
        line.rectTransform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }

        private bool isOpen = false;
        

    public void TogglePanel()
    {
        // isOpen = !isOpen;
        memoryStorage.SetActive(true);
    }

    public void ClosePanel()
    {
        isOpen = false;
        memoryStorage.SetActive(false);
    }
}
