using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        // ทำให้ MusicManager มีอันเดียวในเกม (Singleton)
        if (instance == null) instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // ตั้งให้เล่นวนลูปเป็น Ambient
    }

    public void ChangeMusic(AudioClip newTrack, float fadeTime = 1.5f)
    {
        if (audioSource.clip == newTrack) return; // ถ้าเป็นเพลงเดิมอยู่แล้วไม่ต้องทำอะไร
        StartCoroutine(FadeMusic(newTrack, fadeTime));
    }

    IEnumerator FadeMusic(AudioClip newTrack, float fadeTime)
    {
        float startVolume = audioSource.volume;

        // 1. Fade Out เพลงเก่า
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / (fadeTime / 2);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newTrack;
        audioSource.Play();

        // 2. Fade In เพลงใหม่
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / (fadeTime / 2);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}