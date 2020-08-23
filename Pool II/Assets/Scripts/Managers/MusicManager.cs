using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioClip music;
    public AudioMixerGroup mixer;

    private AudioSource source1;
    private AudioSource source2;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        source1 = gameObject.AddComponent<AudioSource>();
        source2 = gameObject.AddComponent<AudioSource>();

        source1.clip = music;
        source2.clip = music;

        source1.outputAudioMixerGroup = mixer;
        source2.outputAudioMixerGroup = mixer;

        source1.playOnAwake = false;
        source2.playOnAwake = false;

        source1.Play();
    }


}
