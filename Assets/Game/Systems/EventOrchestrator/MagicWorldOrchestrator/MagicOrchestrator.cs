using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicOrchestrator : EventOrchestrator {
    private bool firstTimeInMagic = true;
    public bool leftCatTail = false;

    private MagicOrchestratorData castedData;
    public override void Setup(OrchestratorDataSO data) {
        base.Setup(data);
        castedData = (MagicOrchestratorData)data;
    }
    public override void HandleScenes() {
        if (Player.inst != null) Player.inst.controller.TeleportTo(GetSpawnPosition());
        //Handle per-scene logic
        switch (SceneManager.GetActiveScene().name) {
            case "Forest":
                HandleForest();
                break;
        }
    }

    private void HandleForest() {
        if (firstTimeInMagic) {
            DialogueSystem.inst.StartDialogue(castedData.FirstMagicDialogue);
            firstTimeInMagic = false;
        }
    }
}
