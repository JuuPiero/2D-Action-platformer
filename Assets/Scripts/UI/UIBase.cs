

using UnityEngine.UIElements;
using UnityEngine;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] protected UIDocument _document;
    protected VisualElement _root;

    protected void Awake()
    {
        _document = GetComponent<UIDocument>();
        _root = _document?.rootVisualElement;
    }
}