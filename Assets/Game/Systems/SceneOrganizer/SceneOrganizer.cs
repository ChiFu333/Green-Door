using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOrganizer : MonoBehaviour {
    [Header("Holders")]
    [SerializeField] private Transform managerHolder;
    [SerializeField] private Transform uiHolder;
    [Header("Orchestrator")]
    [SerializeField] private OrchestratorDataSO orchestratorData;
    [Header("Inventory")]
    [SerializeField] private Canvas inventoryCanvasPrefab;
    [SerializeField] private GameObject slotsHolderPrefab;
    [SerializeField] private ItemSlot slotPrefab;
    [SerializeField] private List<ItemCombinationSO> itemCombinations = new List<ItemCombinationSO>();
    [Header("Dialogue")]
    [SerializeField] private float symbolDelay = 0.02f;
    [SerializeField] private Canvas dialogueCanvasPrefab;
    [SerializeField] private GameObject textPanelPrefab;
    [SerializeField] private GameObject imageBoxPrefab;

    private void Awake() {
        //Create GameManager if not present
        if (GameManager.inst == null) InstantiateManager("GameManager", typeof(GameManager), false);
        SetupOrchestrator(orchestratorData);
        SetupInventory();
        SetupInteractionSystem();
        SetupDialogueSystem();
        
    }

    private void SetupOrchestrator(OrchestratorDataSO data) {
        EventOrchestrator orchestrator = InstantiateManager("Orchestrator", Type.GetType(data.orchestratorType), false).GetComponent<EventOrchestrator>();
        orchestrator.Setup(data);
    }

    private void SetupInventory() {
        //Create UI
        Canvas inventoryCanvas = CreateCanvas("Inventory canvas", inventoryCanvasPrefab.gameObject);
        GameObject slotsHolder = Instantiate(slotsHolderPrefab, inventoryCanvas.transform);
        //Create manager
        PlayerInventory inventory = InstantiateManager("Inventory system",typeof(PlayerInventory)).GetComponent<PlayerInventory>();
        inventory.Setup(slotsHolder.transform, slotPrefab, itemCombinations);
        inventory.InitializeSlots();
        //Load items back
        inventory.LoadState();
    }

    private void SetupInteractionSystem() {
        InstantiateManager("Interaction system", typeof(InteractionManager));
    }

    private void SetupDialogueSystem() {
        //Create UI
        Canvas dialogueCanvas = CreateCanvas("Dialogue canvas", inventoryCanvasPrefab.gameObject);
        GameObject textPanel = Instantiate(textPanelPrefab, dialogueCanvas.transform);
        GameObject leftImageBox = Instantiate(imageBoxPrefab, dialogueCanvas.transform);
        GameObject rightImageBox = Instantiate(imageBoxPrefab, dialogueCanvas.transform);
        //Position image boxes
        leftImageBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-730, 0);
        rightImageBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(730, 0);
        //Hide created UI
        dialogueCanvas.gameObject.SetActive(false);
        //Create manager
        DialogueSystem dialogueSystem;
        if (DialogueSystem.inst == null) {
            dialogueSystem = InstantiateManager("Dialogue system", typeof(DialogueSystem),false).GetComponent<DialogueSystem>();
        } else {
            dialogueSystem = DialogueSystem.inst;
        }
        dialogueSystem.Setup(symbolDelay, dialogueCanvas, textPanel, leftImageBox, rightImageBox);
    }

    private Canvas CreateCanvas(string name, GameObject canvasPrefab) {
        Canvas canvas = Instantiate(canvasPrefab, uiHolder).GetComponent<Canvas>();
        canvas.worldCamera = ScreenUtils.GetCamera();
        canvas.gameObject.name = name;
        return canvas;
    }

    private GameObject InstantiateManager(string name, Type componentType, bool doParent = true) {
        GameObject newObject = new GameObject(name);
        if (doParent) newObject.transform.parent = managerHolder;
        newObject.AddComponent(componentType);
        return newObject;
    }

    private void Start() {
        EventOrchestrator.inst.HandleScenes();
    }
}
