using UnityEngine;

public class BaseInteractable : MonoBehaviour
{
    public GameObject interactionInfo;

    public void SetInteractionPopup(bool pop)
    {
        if (interactionInfo != null)
            interactionInfo.SetActive(pop);
    }
}
