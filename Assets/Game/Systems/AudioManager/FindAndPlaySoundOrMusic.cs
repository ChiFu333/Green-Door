using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndPlaySoundOrMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    private void Start()
    {
        PlayMusic(musicClip);
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
    }
}
