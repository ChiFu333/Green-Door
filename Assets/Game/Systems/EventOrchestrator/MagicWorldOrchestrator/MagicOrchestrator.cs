using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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
        { "Knife", false },
        { "Bucket", false },
        { "WaterBucket",false },
        { "FishingRod", false },
        { "Fish", false },
        { "Mushroom", false},
        { "Key", false },
        { "DELETEKEY", false }
        
    };
    public Dictionary<string, bool> events = new Dictionary<string, bool>()
    {
        { "firstTimeWithCat", false },
        { "firstTimeInHouseWithCat", false },
        { "Ladder", false},
        { "LadderBroken", false},
        { "LadderWorking", false},
        { "BirdRemoved", false},
        { "TreeWatered", false},
        { "TeaIsPoisoned", false},
        { "CatIsSleeping", false},
        { "BasementIsFound", false },
        { "LockIsUnlock", false }
    };
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
                HandleAttic();
                break;
            case "Kitchen":
                HandleKitchen();
                break;
            case "Bedroom":
                HandleBedroom();
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
        if (items["Fish"]) Destroy(InteractionManager.inst.GetClickable("FishPlace").gameObject);
    }
    private void HandleHut()
    {

        if (!events["firstTimeWithCat"])
        {
            DialogueSystem.inst.StartDialogue(castedData.FirstTimeWithCat);
            Destroy(GameObject.Find("Forest"));
            Destroy(GameObject.Find("WiseWood"));
            Destroy(GameObject.Find("HutBack"));
            events["firstTimeWithCat"] = true;
        }
        else
        {
            Destroy(GameObject.Find("Kitchen"));
        }
        if (events["CatIsSleeping"])
        {
            Destroy(GameObject.Find("CatSitting"));
            if (items["DELETEKEY"]) PlayerInventory.inst.RemoveItem(castedData.key);
        }
        else
        {
            Destroy(GameObject.Find("CatSitting1"));
        }
        if (items["Key"])
        {
            Destroy(GameObject.Find("KeyOnCat"));
        }
    }
    private void HandleHutBack()
    {
        if (!events["Ladder"])
        {
            GameObject.Find("LadderBroken").gameObject.SetActive(false);
            GameObject.Find("LadderWorking").gameObject.SetActive(false);
        }
        else if (!events["LadderBroken"])
        {
            GameObject.Find("Ladder").gameObject.SetActive(false);
            GameObject.Find("LadderWorking").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Ladder").gameObject.SetActive(false);
            GameObject.Find("LadderBroken").gameObject.SetActive(false);
        }
        if (items["WaterBucket"]) Destroy(InteractionManager.inst.GetClickable("Well").gameObject);
    }
    private void HandleWiseTree()
    {
        if (!events["BirdRemoved"])
        {
            GameObject.Find("WiseFace").SetActive(false);
            GameObject.Find("Mushroom").SetActive(false);
            GameObject.Find("Fish").SetActive(false);
            GameObject.Find("BirdOnEarth").SetActive(false);
            GameObject.Find("WoodObserve2").SetActive(false);
        }
        else if (!events["TreeWatered"])
        {
            GameObject.Find("WiseFace").SetActive(false);
            GameObject.Find("Mushroom").SetActive(false);
            GameObject.Find("BirdOnOak").SetActive(false);
            GameObject.Find("WoodObserve").SetActive(false);
            GameObject.Find("FishObserver").SetActive(false);
        }
        else
        {
            GameObject.Find("FishObserver").SetActive(false);
            GameObject.Find("WoodObserve").SetActive(false);
            GameObject.Find("WoodObserve2").SetActive(false);
            GameObject.Find("BirdOnOak").SetActive(false);
            GameObject.Find("SadFace").SetActive(false);
            if (items["Mushroom"]) Destroy(InteractionManager.inst.GetClickable("Mushroom").gameObject);
        }
    }
    private void HandleAttic()
    {
        if (items["FishingRod"]) Destroy(InteractionManager.inst.GetClickable("FishingRod").gameObject);
        if (events["TeaIsPoisoned"] && !events["CatIsSleeping"])
        {
            DialogueSystem.inst.StartDialogue(castedData.DrinkAndSleep);
            events["CatIsSleeping"] = true;
        }
    }
    private void HandleKitchen()
    {
        if (items["Knife"] || !events["firstTimeInHouseWithCat"]) Destroy(InteractionManager.inst.GetClickable("Knife").gameObject);
        if (!events["firstTimeInHouseWithCat"])
        {
            Destroy(GameObject.Find("Transitions"));
            
            DialogueSystem.inst.StartDialogue(castedData.InKitchen);
            events["firstTimeInHouseWithCat"] = true;
        }
        else
        {
            GameObject.Find("Trigger1").SetActive(false);
            GameObject.Find("CatCooking").SetActive(false);
            GameObject.Find("Observe1").SetActive(false);
        }
    }
    private void HandleBedroom()
    {
        if (items["Bucket"]) Destroy(InteractionManager.inst.GetClickable("Bucket").gameObject);
        if (!events["BasementIsFound"])
        {
            GameObject.Find("Trap").SetActive(false);
            GameObject.Find("Basement").SetActive(false);
        }
        else if (!events["LockIsUnlock"])
        {
            GameObject.Find("Carpet").SetActive(false);
            GameObject.Find("Basement").SetActive(false);
        }
        else
        {
            GameObject.Find("Carpet").SetActive(false);
            GameObject.Find("Trap").SetActive(false);
        }
    }
}
