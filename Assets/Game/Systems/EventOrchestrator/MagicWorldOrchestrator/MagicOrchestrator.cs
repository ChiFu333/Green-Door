using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicOrchestrator : EventOrchestrator {
    private bool firstTimeInMagic = true;
    public Dictionary<string, bool> items = new Dictionary<string, bool>()
    {
        { "Cattail", false},
        { "Cattail2", false},
        { "Rock", false},
        { "Berry", false},
        { "Knife", false }
    };
    private bool firstTimeWithCat = true;
    private bool firstTimeInHouseWithCat = true;

    public MagicOrchestratorData castedData;
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
            case "Lake":
                HandleLake();
                break;
            case "Hut":
                HandleHut();
                break;
            case "WiseTree":
                HandleWiseTree();
                break;
            case "HutBack":
                HandleHutBack();
                break;
            case "Attic":
                HandleLake();
                break;
            case "Kitchen":
                HandleKitchen();
                break;
        }
    }

    private void HandleForest() {
        if (firstTimeInMagic) {
            DialogueSystem.inst.StartDialogue(castedData.FirstMagicDialogue);
            firstTimeInMagic = false;
        }
        if (items["Berry"]) Destroy(InteractionManager.inst.GetClickable("Berry").gameObject);
    }
    private void HandleLake()
    {
        if (items["Cattail"]) Destroy(InteractionManager.inst.GetClickable("Cattail").gameObject);
        if (items["Cattail2"]) Destroy(InteractionManager.inst.GetClickable("Cattail2").gameObject);
        if (items["Rock"]) Destroy(InteractionManager.inst.GetClickable("Rock").gameObject);
    }
    private void HandleHut()
    {
        if(firstTimeWithCat)
        {
            DialogueSystem.inst.StartDialogue(castedData.FirstTimeWithCat);
            Destroy(GameObject.Find("Forest"));
            Destroy(GameObject.Find("WiseWood"));
            Destroy(GameObject.Find("HutBack"));
            firstTimeWithCat = false;
        }
        else
        {
            Destroy(GameObject.Find("Kitchen"));
        }
    }
    private void HandleHutBack()
    {

    }
    private void HandleWiseTree()
    {

    }
    private void Attic() { }
    private void HandleKitchen()
    {
        if (items["Knife"] || firstTimeInHouseWithCat) Destroy(InteractionManager.inst.GetClickable("Knife").gameObject);
        if(firstTimeInHouseWithCat)
        {
            Destroy(GameObject.Find("Transitions"));
            DialogueSystem.inst.StartDialogue(castedData.InKitchen);
        }
    }
}
