using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopMenuUI : MonoBehaviour
{
    public Button menuButton;

    void Awake()
    {
        menuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
    }
}