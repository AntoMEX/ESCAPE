using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] AudioSource audioPlayer;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        } 
        instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        audioPlayer.PlayOneShot(clip);
    }
}
