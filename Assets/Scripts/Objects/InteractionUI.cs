using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public Button interactButton;
    public TextMeshProUGUI interactText;

    public void SetText(string text)
    {
        interactButton?.gameObject?.SetActive(true);
        interactText.text = text;
    }

    public void Hide()
    {
        interactButton?.gameObject?.SetActive(false);
    }    
}