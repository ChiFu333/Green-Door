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
    private void Awake() {
        SetupInventory();
        SetupInteractionSystem();
    }

    private void SetupInventory() {
        //Create UI
        Canvas inventoryCanvas = Instantiate(inventoryCanvasPrefab,uiHolder).GetComponent<Canvas>();
        inventoryCanvas.worldCamera = ScreenUtils.GetCamera();
        GameObject slotsHolder = Instantiate(slotsHolderPrefab, inventoryCanvas.transform);
        //Create manager
        PlayerInventory inventory = InstantiateManager("Inventory system",typeof(PlayerInventory)).GetComponent<PlayerInventory>();
        inventory.Setup(slotsHolder.transform, slotPrefab);
    }

    private void SetupInteractionSystem() {
        InstantiateManager("Interaction system",typeof(InteractionManager));
    }

    private GameObject InstantiateManager(string name, Type componentType) {
        GameObject newObject = new GameObject(name);
        newObject.transform.parent = managerHolder;
        newObject.AddComponent(componentType);
        return newObject;
    }
}
