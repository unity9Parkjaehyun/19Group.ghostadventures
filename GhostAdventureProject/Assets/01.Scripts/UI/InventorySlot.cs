using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI clueName;

    public void Setup(ClueData clue)
    {
        icon.sprite = clue.clue_Icon;
        clueName.text = clue.clue_Name;
    }
}
