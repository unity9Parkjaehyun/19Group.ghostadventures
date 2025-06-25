using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypewriterDialogue : MonoBehaviour
{
    public TimelineControl timelineControl;
    public GameObject dialoguePanel; // 대화창 패널
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public string[] dialogues; // 여러 문장
    private int index = 0;
    private bool isTyping = false;
    private bool skipTyping = false;

    void Start()
    {
        dialogueText.text = "";
    }

    void Awake()
    {
        if (timelineControl == null)
            timelineControl = GameObject.FindObjectOfType<TimelineControl>();

    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            if (isTyping)
            {
                skipTyping = true;
            }
            else
            {
                NextLine();
            }
        }
    }
    public void StartDialogue()
    {

        dialoguePanel.SetActive(true); // 대화창 열기
        StartCoroutine(TypeLine());

    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in dialogues[index].ToCharArray())
        {
            if (skipTyping)
            {
                dialogueText.text = dialogues[index];
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        // 대사가 모두 끝나면 타임라인 재생
        

        isTyping = false;
        skipTyping = false;
    }


    void NextLine()
    {
        if (index < dialogues.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // 대사가 모두 끝나면 타임라인 재생
            dialogueText.text = "";
            GameObject.Find("TimelineManager").GetComponent<TimelineControl>().ResumeTimeline();
            dialoguePanel.SetActive(false); // 대화창 닫기
        }
    }
}
