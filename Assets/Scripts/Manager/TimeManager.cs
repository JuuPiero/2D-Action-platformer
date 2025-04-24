using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance {get; private set; }

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }
    public void SlowDownTime(float slowFactor, float duration)
    {
        Time.timeScale = slowFactor;  // Giảm tốc độ thời gian
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Điều chỉnh physics theo timeScale
        StartCoroutine(ResetTime(duration));
    }

    private IEnumerator ResetTime(float duration)
    {
        yield return new WaitForSecondsRealtime(duration); // Đợi trong thời gian thực
        Time.timeScale = 1f;  // Khôi phục tốc độ thời gian bình thường
        Time.fixedDeltaTime = 0.02f; // Reset physics
    }
}