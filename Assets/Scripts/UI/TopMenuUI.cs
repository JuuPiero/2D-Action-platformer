using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopMenuUI : MonoBehaviour
{
    public Button menuButton;
    public Button inventoryButton;
    void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}