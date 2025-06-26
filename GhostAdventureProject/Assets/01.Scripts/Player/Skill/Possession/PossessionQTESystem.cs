using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionQTESystem : Singleton<PossessionQTESystem>
{
    [SerializeField] private QTEUI QTEUI;
    private BasePossessable currentTarget;
    public bool isRunning { get; private set; } = false;

    private void Start()
    {
        QTEUI.gameObject.SetActive(false);
    }
    public void StartQTE(BasePossessable target)
    {
        currentTarget = target;
        isRunning = true;
        Debug.Log("Starting QTE");
        QTEUI.ShowQTEUI();
    }

    public void HandleQTEResult(bool success)
    {
        isRunning = false;
        if (success)
        {
            Debug.Log("QTE succeeded");
            currentTarget?.OnQTESuccess();
        }
        else
        {
            Debug.Log("QTE failed");
            currentTarget?.OnQTEFailure();
        }
    }
}
