using UnityEngine;
using UnityEngine.UI;

public interface IInteractable
{
    Image Icon { get; set; }
    void Interact(GameObject interactor);
}