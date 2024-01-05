using UnityEngine;

public class FindAndPlaySoundOrMusic : MonoBehaviour {
    [SerializeField] AudioClip clip;
    [SerializeField] float volume = 1;
    [SerializeField] bool isMusic = true;
    private void Start() {
        if (isMusic) PlayMusic(clip);
    }
    public void PlaySound(AudioClip clip) {
        AudioManager.inst.Play(new AudioQuery(clip));
    }
    public void PlayMusic(AudioClip clip) {
        AudioManager.inst.PlayMusic(clip);
        if (volume == 0) volume = 1;
        AudioManager.inst.SetMusicVolume(volume);
    }
}
