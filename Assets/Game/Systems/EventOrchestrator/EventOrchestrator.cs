using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventOrchestrator : MonoBehaviour {
    public static EventOrchestrator inst { get; private set; }
    public string previousRoom { get; set; }
    private readonly Dictionary<string, Vector2> spawnPositions = new Dictionary<string, Vector2>();
    public virtual void Setup(OrchestratorDataSO data) { ResetState(); }
    public void AddSpawnPosition(string room, Vector2 position) {
        if (spawnPositions.ContainsKey(room)) return;
        spawnPositions.Add(room, position);
    }

    public virtual void ResetState() { }

    public virtual void HandleScenes() { }

    protected Vector2 GetSpawnPosition() {
        if (string.IsNullOrEmpty(previousRoom) || !spawnPositions.ContainsKey(previousRoom)) {
            return Player.inst.controller.GetPosition();
        }
        return spawnPositions[previousRoom];
    }

    private void ClearSpawnPositions() {
        spawnPositions.Clear();
    }

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += (scene, loadMode) => ClearSpawnPositions();
        }
    }
}
