using System;
using UnityEngine;

public class SceneOrganizer : MonoBehaviour {
    [Header("Holders")]
    [SerializeField] private Transform managerHolder;
    [SerializeField] private Transform uiHolder;
    [Header("Inventory")]
    [SerializeField] private Canvas inventoryCanvasPrefab;
    [SerializeField] private GameObject slotsHolderPrefab;
    [SerializeField] private ItemSlot slotPrefab;
    [Header("Dialogue")]
    [SerializeField] private float symbolDelay = 0.02f;
    [SerializeField] private Canvas dialogueCanvasPrefab;
    [SerializeField] private GameObject textPanelPrefab;
    [SerializeField] private GameObject imageBoxPrefab;
    [SerializeField] private DialogueSO sampleDialogue; 

    private void Awake() {
        SetupInventory();
        SetupInteractionSystem();
        SetupDialogueSystem();
    }

    private void SetupInventory() {
        //Create UI
        Canvas inventoryCanvas = CreateCanvas("Inventory canvas", inventoryCanvasPrefab.gameObject);
        GameObject slotsHolder = Instantiate(slotsHolderPrefab, inventoryCanvas.transform);
        //Create manager
        PlayerInventory inventory = InstantiateManager("Inventory system",typeof(PlayerInventory)).GetComponent<PlayerInventory>();
        inventory.Setup(slotsHolder.transform, slotPrefab);
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
        DialogueSystem dialogueSystem = InstantiateManager("Dialogue system", typeof(DialogueSystem)).GetComponent<DialogueSystem>();
        dialogueSystem.Setup(symbolDelay, dialogueCanvas, textPanel, leftImageBox, rightImageBox);

        dialogueSystem.StartDialogue(sampleDialogue);
    }

    private Canvas CreateCanvas(string name, GameObject canvasPrefab) {
        Canvas canvas = Instantiate(canvasPrefab, uiHolder).GetComponent<Canvas>();
        canvas.worldCamera = ScreenUtils.GetCamera();
        canvas.gameObject.name = name;
        return canvas;
    }

    private GameObject InstantiateManager(string name, Type componentType) {
        GameObject newObject = new GameObject(name);
        newObject.transform.parent = managerHolder;
        newObject.AddComponent(componentType);
        return newObject;
    }
}
