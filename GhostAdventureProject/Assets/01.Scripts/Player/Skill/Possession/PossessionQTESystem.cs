using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionQTESystem : Singleton<PossessionQTESystem>
{
    [SerializeField] private QTEUI QTEUI;
    private PlayerController Player; // 게임매니저 연결 후 수정
    private BasePossessable currentTarget;
    public bool isRunning { get; private set; } = false;

    private void Start()
    {
        QTEUI.gameObject.SetActive(false);
        Player = FindObjectOfType<PlayerController>(); // 게임매니저 연결 후 수정
    }
    public void StartQTE()
    {
        Time.timeScale = 0.3f;
        // UIManager연동되면 스캔 때 까만 배경 활성화
        isRunning = true;
        Debug.Log("Starting QTE");
        QTEUI.ShowQTEUI();
    }

    public void HandleQTEResult(bool success)
    {
        isRunning = false;
        Time.timeScale = 1f;
        // UIManager연동되면 스캔 때 까만 배경 비활성화
        PossessionSystem.Instance.canMove = true;
        if (success)
        {
            Debug.Log("QTE succeeded");
            Player.currentTarget?.OnQTESuccess();
        }
        else
        {
            Debug.Log("QTE failed");
            Player.currentTarget?.OnQTEFailure();
        }
    }
}
