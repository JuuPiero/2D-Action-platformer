using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    void Interact();
    string InteractionPrompt { get; set; }
    float InteractRange { get; set; }
    bool IsPlayerInRange { get; set; }
}