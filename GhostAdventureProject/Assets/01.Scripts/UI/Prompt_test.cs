using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompt_test : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        string[] lines = 
        {
            "안녕하세요. 지금은",
            "프롬프트 테스트 중입니다",
            "잘 작동하나요?"            
        };

        Prompt.Instance.ShowPrompt(lines);
    }
}


