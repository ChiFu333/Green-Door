using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager inst { get; private set; }
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private AudioClip musicClip;
    [Header("Clamping")]
    [SerializeField] private int maxQueriesPerType;
    [SerializeField] private AudioClip avoidClip;
    private readonly Dictionary<AudioClip, int> queriesPlaying = new Dictionary<AudioClip, int>();
    private readonly Stack<PlayingQueryTime> queriesStack = new Stack<PlayingQueryTime>();
    private float musicVolume;
    private float soundVolume;
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic(musicClip);
        }
    }

    public void SetMusicVolume(float value) {
        musicVolume = value;
        musicSource.volume = musicVolume;
    }

    public void SetSoundVolume(float value) {
        soundVolume = value;
        soundsSource.volume = soundVolume;
    }

    public void Play(AudioQuery query) {
        if (query.clip == null) return;
        if (queriesPlaying.ContainsKey(query.clip)) {
            if (query.clip != avoidClip && queriesPlaying[query.clip] >= maxQueriesPerType) return;
        }
        soundsSource.pitch = Random.Range(1 - query.pitchVariance, 1 + query.pitchVariance);
        soundsSource.PlayOneShot(query.clip);
        float timeFinished = Time.time + query.clip.length;
        PlayingQueryTime stamp = new PlayingQueryTime() { clip = query.clip , timestamp = timeFinished };
        if (queriesPlaying.ContainsKey(query.clip)) {
            queriesPlaying[query.clip]++;
            queriesStack.Push(stamp);
        } else {
            queriesPlaying.Add(query.clip, 1);
            queriesStack.Push(stamp);
        }
    }

    public void PlayMusic(AudioClip clip) {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void StopMusic() {
        musicSource.Stop();
    }

    private void CheckStack() {
        if (queriesStack.Count == 0) return;
        if (queriesStack.Peek().timestamp <= Time.time) {
            queriesPlaying[queriesStack.Peek().clip]--;
            queriesStack.Pop();
        }
    }

    private void Update() {
        CheckStack();
    }

    private class PlayingQueryTime {
        public AudioClip clip;
        public float timestamp;
    }
}
