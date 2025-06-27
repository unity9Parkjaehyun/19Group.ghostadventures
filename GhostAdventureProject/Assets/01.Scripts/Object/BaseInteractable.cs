using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    public GameObject interactionInfo;

    void Start()
    {
        if (interactionInfo != null)
            interactionInfo.SetActive(false);
    }

    public void SetInteractionPopup(bool pop)
    {
        if (interactionInfo != null)
            interactionInfo.SetActive(pop);
    }
}
