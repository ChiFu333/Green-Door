using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private string targetScene;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    public void Play() {
        SceneLoader.inst.LoadScene(targetScene,false);
    }

    public void Settings() {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void MusicVolumeSet(float value) {
        AudioManager.inst.SetMusicVolume(value);
        Debug.Log(value);
    }

    public void SoundVolumeSet(float value) {
        AudioManager.inst.SetSoundVolume(value);
    }
    public void DeleteOrchestrator()
    {   
        if(FindFirstObjectByType<WorldOrchestrator>() != null) Destroy(FindFirstObjectByType<WorldOrchestrator>().gameObject);
    }
    public void Exit() {
        Application.Quit(0);
    }

    void Start() {
        soundSlider.onValueChanged.AddListener(SoundVolumeSet);
        musicSlider.onValueChanged.AddListener(MusicVolumeSet);
    }
}
