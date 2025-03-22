using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarUI : MonoBehaviour
{
    // public event Action OnChange;
    [SerializeField] private Player player;
    [SerializeField] public UIDocument uiDocument;
    void Update()
    {
        var root = uiDocument.rootVisualElement;
        var hpBar = root.Query<VisualElement>().Class("current-health").First();
        hpBar.style.width = new Length(player.CurrentHealth * 100 / player.Data.maxHealthPoint, LengthUnit.Percent);
    }
}
