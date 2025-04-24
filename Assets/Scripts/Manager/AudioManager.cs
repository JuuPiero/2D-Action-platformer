using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    // public AudioSource musicSource;
    public AudioSource sfxSource;
    public List<AudioClip> sfxClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);

            foreach (var clip in sfxClips)
            {
                sfxDict[clip.name] = clip;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string name)
    {
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }


    public void StopLoopingSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.loop = false;
            sfxSource.Stop();
        }
    }
}