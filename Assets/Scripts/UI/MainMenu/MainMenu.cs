
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private VisualElement _root;
    void Awake() {
        _document = GetComponent<UIDocument>();
        _root = _document.rootVisualElement;
    }

    void Start() {
        _root.QuerySelector<Button>(".exit-btn")?.RegisterCallback<ClickEvent>(e => {
            Application.Quit();
        });

        _root.QuerySelector<Button>(".start-btn")?.RegisterCallback<ClickEvent>(e => {
            SceneManager.LoadScene("CombatTest");
        });
    }
}