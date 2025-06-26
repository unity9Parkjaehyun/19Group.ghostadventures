using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryStorage : MonoBehaviour
{
    [SerializeField] private GameObject memeoryPanel; // 기억 저장소 UI
    [SerializeField] private Button memoryStorageButton; // 기억 저장소 버튼
    MemoryData memoryData;


    // 기억저장소 버튼을 눌렀을 때
    // 기억 저장소 Panel이 SetActive(true)
    // 기억 저장소에는 기억(MemoryData)이 나열되어있음 - MemoryData의 ID와 BG가 표시됨
    // 기억 클릭시 BG가 확대됨(배경은 BG에 집중할 수 있도록 어두워짐)
    // x 클릭시 다시 기억이 나열되어 있는 곳으로 돌아옴.
    // 또 x 클릭시 Panel SetActive(false)

    // 부정/긍정/가짜 기억 구분은 색으로 표시(미정)
    // 수집 현황 표시 (ex 1/30)


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
