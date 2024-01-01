using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldOrchestrator : EventOrchestrator {
    private bool firstTimeAtHome = true;
    private bool isDoorUnlocked = false;
    private bool isSchoolAlreadyVisited = false;
    private WorldOrchestratorData castedData;

    #region StartHandling
    public override void Setup(OrchestratorDataSO data) {
        base.Setup(data);
        castedData = (WorldOrchestratorData)data;
    }

    public override void HandleScenes() {
        //Handle spawn positioning
        if (Player.inst != null) {
            Debug.Log("If scene is \"CheckTime\" - this is a bug");
            Player.inst.controller.TeleportTo(GetSpawnPosition());
        }
            //Handle per-scene logic
            switch (SceneManager.GetActiveScene().name) {
            case "MyHouse":
                HandleHouse();
                break;
            case "School":
                HandleSchool();
                break;
            case "Street":
                HandleStreet();
                break;
            case "CheckTime":
                HandleTimeCheck();
                break;
        }
    }

    private void HandleHouse() {
        if (firstTimeAtHome) {
            DialogueSystem.inst.StartDialogue(castedData.initialDialogue);
            firstTimeAtHome = false;
        }
    }

    private void HandleSchool() {
        ClickableThing schoolDoorClickable = InteractionManager.inst.GetClickable("schoolDoor");
        if (isSchoolAlreadyVisited) {
            //Should say something
        } else {
            
            schoolDoorClickable.postCallback.AddListener(() => {
                isDoorUnlocked = true;
                isSchoolAlreadyVisited = true;
                DialogueSystem.inst.StartDialogue(castedData.checkTimeDialogue);
            });
            schoolDoorClickable.callback.AddListener(()=> {
                
            });
        }
    }
    private void HandleStreet() {
        if (isDoorUnlocked) {
            Instantiate(castedData.greenDoorPrefab, GameObject.Find("Background").transform);
        } else {
            Instantiate(castedData.streetPropsPrefab);
        }
    }

    private void HandleTimeCheck() {
        
    }
    #endregion
}
