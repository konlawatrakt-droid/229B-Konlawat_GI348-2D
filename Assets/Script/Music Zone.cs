using UnityEngine;

public class MusicZone : MonoBehaviour
{
    public AudioClip zoneMusic; // เพลงที่อยากให้เล่นในโซนนี้
    public float fadeSpeed = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // สั่งให้ Manager เปลี่ยนเพลง
            if (MusicManager.instance != null && zoneMusic != null)
            {
                MusicManager.instance.ChangeMusic(zoneMusic, fadeSpeed);
            }
        }
    }
}