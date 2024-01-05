using UnityEngine;

[CreateAssetMenu(fileName = "WorldData", menuName = "Orchestration/WorldData")]
public class WorldOrchestratorData : OrchestratorDataSO {
    [field: SerializeField] public DialogueSO initialDialogue { get; private set; }
    [field: SerializeField] public DialogueSO checkTimeDialogue { get; private set; }
    [field: SerializeField] public GameObject greenDoorPrefab { get; private set; }
    [field: SerializeField] public GameObject streetPropsPrefab { get; private set; }

    public void CheckTimeDialogueEnded() { SceneLoader.inst.LoadScene("School",true); }
    public void LoadCheckTimeScene() { SceneLoader.inst.LoadSceneDuringDialogue("CheckTime", true); }

    /// MAGIC WORLD ////////////////////////////////////////////////////////////////////////////////

    [field: SerializeField] public DialogueSO FirstMagicDialogue { get; private set; }
    public void PlaySound(AudioClip clip)
    {
        AudioManager AM = FindObjectOfType<AudioManager>();
        AudioQuery AQ = new AudioQuery(clip);
        AM.Play(AQ);
    }
}
