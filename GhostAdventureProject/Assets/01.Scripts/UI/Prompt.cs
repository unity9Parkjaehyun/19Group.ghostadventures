using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prompt : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Button nextButton;

    private Queue<string> dialogQueue = new Queue<string>();
    private System.Action onDialogComplete;

    private void Awake()
    {
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(ShowNextLine);
    }

    public void ShowDialog(string[] lines, System.Action onComplete = null)
    {
        dialogQueue.Clear();
        foreach (var line in lines)
            dialogQueue.Enqueue(line);

        onDialogComplete = onComplete;
        dialogPanel.SetActive(true);
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (dialogQueue.Count > 0)
        {
            string nextLine = dialogQueue.Dequeue();
            dialogText.text = nextLine;
        }
        else
        {
            dialogPanel.SetActive(false);
            onDialogComplete?.Invoke();
        }
    }
}




// // 사용 예시 void TriggerEvent()
// {
//     string[] lines = new string[]
//     {
//         "여기 정말 어두워...",
//         "조심해서 가야겠어."
//     };

//     FindObjectOfType<DialogUI>().ShowDialog(lines, () =>
//     {
//         Debug.Log("✅ 대화 끝! 퀘스트 시작.");
//     });
// }
