using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "MagicData", menuName = "Orchestration/MagicData")]
public class MagicOrchestratorData : OrchestratorDataSO {
    [field: SerializeField] public DialogueSO FirstMagicDialogue { get; private set; }
    [field: SerializeField] public DialogueSO FirstTimeWithCat { get; private set; }
    [field: SerializeField] public DialogueSO InKitchen { get; private set; }
    [field: SerializeField] public DialogueSO GoToFood { get; private set; }
    [field: SerializeField] public DialogueSO CatIsAngry { get; private set; }
    [field: SerializeField] public DialogueSO ThinksAboutMushroom { get; private set; }

    public void ForceExitFromHut()
    {
        SceneLoader.inst.LoadScene("Hut");
    }
    public void PlaySound(AudioClip clip) {
        AudioManager.inst.Play(new AudioQuery(clip));
    }
    ////for events
    public void PlayMushDialog()
    {
        DialogueSystem.inst.StartDialogue(ThinksAboutMushroom);
    }
    public void PushEvent(string name)
    {
        FindFirstObjectByType<MagicOrchestrator>().events[name] = true;
    }
    public void PushItem(string name)
    {
        FindFirstObjectByType<MagicOrchestrator>().items[name] = true;
    }
}
