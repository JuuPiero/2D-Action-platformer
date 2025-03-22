using UnityEngine;
using UnityEngine.UIElements;

public class StaminaBarUI : MonoBehaviour
{
    // public event Action OnChange;
    [SerializeField] private Player _player;
    [SerializeField] public UIDocument uiDocument;

    void Start() {
        _player = FindAnyObjectByType<Player>();
        uiDocument = GetComponent<UIDocument>();
    }

    void Update()
    {
        var root = uiDocument.rootVisualElement;
        var staminaBar = root.Query<VisualElement>().Class("current-stamina").First();
        staminaBar.style.width = new Length(_player.CurrentStamina * 100 / _player.Data.maxStamina, LengthUnit.Percent);
    }
}
