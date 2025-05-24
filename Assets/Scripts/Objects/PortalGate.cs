using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalGate : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // SceneManager.LoadScene(sceneName);
            StartCoroutine(LoadSceneAsync(sceneName));
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Tùy chọn: chờ cho đến khi load xong
        while (!operation.isDone)
        {
            // Có thể hiện tiến độ ở đây: operation.progress (giá trị từ 0 đến 0.9)
            Debug.Log("Loading progress: " + (operation.progress * 100f) + "%");
            yield return null;
        }
    }
}