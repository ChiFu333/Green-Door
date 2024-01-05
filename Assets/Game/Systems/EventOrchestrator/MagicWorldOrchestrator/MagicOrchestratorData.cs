using UnityEngine;

[CreateAssetMenu(fileName = "MagicData", menuName = "Orchestration/MagicData")]
public class MagicOrchestratorData : OrchestratorDataSO {
    [field: SerializeField] public DialogueSO FirstMagicDialogue { get; private set; }

    public void PlaySound(AudioClip clip) {
        AudioManager.inst.Play(new AudioQuery(clip));
    }
}
