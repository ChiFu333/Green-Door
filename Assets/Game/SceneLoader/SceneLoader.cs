using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private float timeToFade = 0.5f;
    private Animation anim;
    public AnimationClip fadeoutClip;
    public void PushLoad(int id) => StartCoroutine(FadeAndLoad(id));
    public void Start()
    {
        anim = GetComponent<Animation>();
    }
    public IEnumerator FadeAndLoad(int id)
    {
        anim.clip = fadeoutClip;
        anim.Play();
        yield return new WaitForSeconds(timeToFade);
        SceneManager.LoadScene(id);
    }
}
