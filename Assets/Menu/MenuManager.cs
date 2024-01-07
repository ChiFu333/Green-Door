using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private string targetScene;
    [SerializeField] private GameObject settingsPanel;
    

    public void Play() {
        SceneLoader.inst.LoadScene(targetScene,false);
    }

    public void Settings() {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void MusicVolumeSet(float value) {
        AudioManager.inst.SetMusicVolume(value);
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
}
