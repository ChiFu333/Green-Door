using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "Orchestration/MagicData")]
public class MagicOrchestratorData : OrchestratorDataSO {
    [field: SerializeField] public DialogueSO FirstMagicDialogue { get; private set; }
    [field: SerializeField] public DialogueSO FirstTimeWithCat { get; private set; }
    [field: SerializeField] public DialogueSO InKitchen { get; private set; }
    [field: SerializeField] public DialogueSO GoToFood { get; private set; }
    [field: SerializeField] public DialogueSO CatIsAngry { get; private set; }

    public void ForceExitFromHut()
    {
        SceneLoader.inst.LoadScene("Hut");
    }
    public void PlaySound(AudioClip clip) {
        AudioManager.inst.Play(new AudioQuery(clip));
    }
}
