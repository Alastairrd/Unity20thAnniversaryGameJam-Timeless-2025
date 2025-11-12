using UnityEngine;
[RequireComponent (typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    float PitchRandomizerOffset;

    float startingPitch;

    private AudioSource AudioSource;

    [SerializeField]
    private AudioClip[] AudioClips;

    bool PlayOnStart;

    private float lastAudioTime = 0f; // Track the last known play time

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        PlayOnStart = AudioSource.playOnAwake;
        startingPitch = AudioSource.pitch;
        RandomizeSound();
    }

    void Start()
    {
        if (PlayOnStart)
        {
            PlaySound();
        }
    }

    void Update()
    {
        // Check if the audio clip has completed a loop
        if (AudioSource.isPlaying && AudioSource.loop)
        {
            if (AudioSource.time < lastAudioTime) // When time resets to zero, it's a new loop
            {
                PlaySound();
            }
            lastAudioTime = AudioSource.time; // Update last known play time
        }
    }

    public void PlaySound()
    {
        RandomizeSound();
        AudioSource.Play();
    }

    void RandomizeSound()
    {
        AudioSource.clip = AudioClips[Random.Range(0, AudioClips.Length)];
        AudioSource.pitch = Mathf.Clamp(Random.Range(startingPitch - PitchRandomizerOffset, startingPitch + PitchRandomizerOffset), .1f, 3f);
    }
}

