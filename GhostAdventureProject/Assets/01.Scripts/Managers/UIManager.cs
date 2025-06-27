using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    //ui창 열고 닫기
    //ui상태관리(어떤ui켜져있는지 추적)
    //시각효과 제어(미정)


    // 각 UI에 연결 ======================================================
    [SerializeField] private SoulGauge soulGauge; // 영혼에너지 
    [SerializeField] private Prompt prompt; // 프롬프트
    [SerializeField] private QTEUI qte1;// QTE
    [SerializeField] private GameObject scanUI;
    [SerializeField] private MemoryStorage memoryStorage;// 기억저장소
    [SerializeField] private Inventory_Player inventory_player; // 인벤토리-플레이어
    [SerializeField] private InventoryExpandViewer inventoryExpandViewer; // 인벤토리 확대뷰어

    // 외부 접근용
    public SoulGauge SoulGaugeUI => soulGauge;
    public Prompt PromptUI => prompt;
    public QTEUI QTE_UI => qte1;
    public MemoryStorage MemoryStorageUI => memoryStorage;
    public Inventory_Player Inventory_PlayerUI => inventory_player;
    public InventoryExpandViewer InventoryExpandViewerUI => inventoryExpandViewer;
    // ==================================================================
    

    private void Start()
    {

    }
}
