using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader inst { get; private set; }

    [SerializeField] private float timeToFade = 0.5f;
    [SerializeField] private AnimationClip fadeoutClip;
    private Animation anim => GetComponent<Animation>();

    public void LoadScene(int id) => StartCoroutine(FadeAndLoad(SceneUtility.GetScenePathByBuildIndex(id)));
    public void LoadScene(string scene) => StartCoroutine(FadeAndLoad(scene));

    private IEnumerator FadeAndLoad(string sceneName) {
        anim.clip = fadeoutClip;
        anim.Play();
        EventOrchestrator.inst.previousRoom = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(timeToFade);
        SceneManager.LoadScene(sceneName);
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
        }
    }
}
