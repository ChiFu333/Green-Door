using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private string targetScene;
    [SerializeField] private GameObject settingsPanel;
    

    public void Play() {
        SceneLoader.inst.LoadScene(targetScene);
    }

    public void Settings() {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void MusicVolumeSet(float value) {
        
    }

    public void SoundVolumeSet(float value) {

    }

    public void Exit() {
        Application.Quit(0);
    }
}
