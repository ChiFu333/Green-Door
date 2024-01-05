using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader inst { get; private set; }

    [SerializeField] private float timeToFade = 0.5f;
    [SerializeField] private AnimationClip fadeoutClip;
    private Animation anim => GetComponent<Animation>();

    public void LoadScene(int id, bool saveState = false) => StartCoroutine(FadeAndLoad(SceneUtility.GetScenePathByBuildIndex(id), false, saveState));
    public void LoadScene(string scene, bool saveState = false) => StartCoroutine(FadeAndLoad(scene, false, saveState));
    public void LoadSceneDuringDialogue(string scene, bool saveState) {
        DialogueSystem.inst.Pause(true);
        StartCoroutine(FadeAndLoad(scene, true, saveState));
    }

    private IEnumerator FadeAndLoad(string sceneName, bool doUnpause, bool saveState) {
        //Saving state
        if (PlayerInventory.inst != null) PlayerInventory.inst.SaveState();
        //Animation
        anim.clip = fadeoutClip;
        anim.Play();
        if (EventOrchestrator.inst != null) EventOrchestrator.inst.previousRoom = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(timeToFade);
        SceneManager.LoadScene(sceneName);
        if (doUnpause) DialogueSystem.inst.Pause(false);
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
        }
    }
}
