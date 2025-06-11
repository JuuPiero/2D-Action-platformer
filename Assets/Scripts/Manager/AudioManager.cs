using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public GameObject audioSourcePrefab;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource soundSource;

    public List<AudioClip> sounds = new();
    private Dictionary<string, AudioClip> sfxDict = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach (var clip in sounds)
            {
                sfxDict[clip.name] = clip;
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(string name)
    {
        // if (sfxSource.isPlaying) return;
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySound(string name)
    {
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {
            soundSource.PlayOneShot(clip);
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

    public void PlaySoundOneShot(string name, Vector3 position)
    {
        if (sfxDict.TryGetValue(name, out AudioClip clip))
        {
            GameObject audioSourceGO = Instantiate(audioSourcePrefab, position, Quaternion.identity);
            AudioSource audioSource = audioSourceGO.GetComponent<AudioSource>();
            //soundSource.PlayOneShot(clip);
            audioSource.clip = clip;
            audioSource.PlayOneShot(clip);
            Destroy(audioSourceGO, clip.length);
        }
    }
}