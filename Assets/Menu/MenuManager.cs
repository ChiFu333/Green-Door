using UnityEngine;

public class MenuManager : MonoBehaviour {
    [SerializeField] private string targetScene;
    public void Play() {
        SceneLoader.inst.LoadScene(targetScene);
    }

    public void Settings() {
        //Open settings menu
    }

    public void Exit() {
        Application.Quit(0);
    }
}
