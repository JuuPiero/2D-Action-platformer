using UnityEngine;
using UnityEngine.UIElements;

public class ManaBarUI : MonoBehaviour
{
    // public event Action OnChange;
    [SerializeField] private Player _player;
    [SerializeField] public UIDocument uiDocument;

    void Start()
    {
        _player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        var root = uiDocument.rootVisualElement;
        var manaBar = root.Query<VisualElement>().Class("current-mana").First();
        manaBar.style.width = new Length(_player.CurrentMana * 100 / _player.Data.maxMana, LengthUnit.Percent);
    }
}
