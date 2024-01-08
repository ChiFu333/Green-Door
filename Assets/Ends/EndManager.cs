using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private ItemDataSO key;
    public void Play()
    {
        SceneLoader.inst.LoadScene(targetScene, false);
    }
    public void DeleteOrchestrator()
    {
        if (FindFirstObjectByType<WorldOrchestrator>() != null) Destroy(FindFirstObjectByType<WorldOrchestrator>().gameObject);
    }
    public void PlaySound(AudioClip clip)
    {
        AudioManager.inst.Play(new AudioQuery(clip));
    }
    public void ResetProgress()
    {
        MagicOrchestrator MO = FindObjectOfType<MagicOrchestrator>();
        if (MO != null)
        {
            MO.items["Key"] = false;
            MO.items["DELETEKEY"] = true;
            MO.events["LockIsUnlock"] = false;
        }
    }
}
