using UnityEngine;

public class SFXManager_Exception : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
