using UnityEngine;

public class SpawnAnchor : MonoBehaviour {
    [SerializeField] private string previousRoom;
    private void Start() {
        EventOrchestrator.inst.AddSpawnPosition(previousRoom, transform.position);
    }
}
