using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prompt : Singleton<Prompt>
{
    private GameObject PromptPanel; // 프롬프트 이미지
    private TMP_Text PromptText; // 프롬프트 텍스트

    private Queue<string> PromptQueue = new Queue<string>();
    private System.Action onDialogComplete;
    private bool isActive = false;

    private void Start()
    {
        PromptPanel = gameObject;
        PromptText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        
        PromptPanel.SetActive(false);
    }

    public void ShowPrompt(string[] lines) //, System.Action onComplete = null
    {
        PromptQueue.Clear();
        foreach (var line in lines)
            PromptQueue.Enqueue(line);

        // onDialogComplete = onComplete;
        PromptPanel.SetActive(true);
        isActive = true;
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (PromptQueue.Count > 0)
        {
            string nextLine = PromptQueue.Dequeue();
            PromptText.text = nextLine;
        }
        else
        {
            PromptPanel.SetActive(false);
            isActive = false;
            // onDialogComplete?.Invoke();
        }
    }

    private void Update()
    {
        if(!isActive) return;
        
        if(Input.GetMouseButtonDown(0))
            ShowNextLine();
    }
}



