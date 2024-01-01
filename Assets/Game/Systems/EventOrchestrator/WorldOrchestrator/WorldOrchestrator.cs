using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldOrchestrator : EventOrchestrator {
    private bool firstTimeAtHome = true;
    private bool isDoorUnlocked = false;
    private bool isSchoolAlreadyVisited = false;
    private WorldOrchestratorData castedData;

    #region Actions
    public void UnlockDoor() {
        isDoorUnlocked = true;
        isSchoolAlreadyVisited = true;
    }
    #endregion

    #region StartHandling
    public override void Setup(OrchestratorDataSO data) {
        base.Setup(data);
        castedData = (WorldOrchestratorData)data;
    }

    public override void HandleScenes() {
        //Handle spawn positioning
        if (Player.inst != null) Player.inst.controller.TeleportTo(GetSpawnPosition());
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
                SceneLoader.inst.LoadScene("CheckTime");
            });
            schoolDoorClickable.callback.AddListener(UnlockDoor);
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
        DialogueSystem.inst.StartDialogue(castedData.checkTimeDialogue);
    }
    #endregion
}
