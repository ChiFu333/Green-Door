using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndPlaySoundOrMusic : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float volume = 1;
    [SerializeField] bool isMusic = true;
    private void Start()
    {
        if(isMusic) PlayMusic(clip);
    }
    public void PlaySound(AudioClip clip)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AudioQuery AQ = new AudioQuery(clip);
        AM.Play(AQ);
    }
    public void PlayMusic(AudioClip clip)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AM.PlayMusic(clip);
        if (volume == 0) volume = 1;
        AM.SetMusicVolume(volume);
    }
}
