using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    private const int slotsAmount = 4;
    [SerializeField] private Transform slotsHolder;
    [SerializeField] private ItemSlot slotPrefab;

    private readonly List<ItemSlot> slots = new List<ItemSlot>();

    public void PickupItem(Item item) {
        //TODO: Check if item can be combined with something else
        ItemSlot targetSlot = FindFreeSlot();
        targetSlot.SetItem(item);
    }

    public void RemoveItem(ItemDataSO itemData, bool removeAll = false) {
        for (int i = 0; i < slotsAmount; i++) {
            if (slots[i].GetItem().data.Equals(itemData)) {
                slots[i].RemoveItem();
                if (!removeAll) return;
            }
        }
    }

    private ItemSlot FindFreeSlot() {
        for (int i = 0; i < slotsAmount; i++) {
            if (slots[i].IsFree()) return slots[i];
        }
        throw new System.Exception("Not enough slots!");
    }

    private void Start() {
        InitializeSlots();
    }

    private void InitializeSlots() {
        for (int i = 0; i < slotsAmount; i++) {
            GameObject slotObject = Instantiate(slotPrefab.gameObject, slotsHolder, false);
            ItemSlot slot = slotObject.GetComponent<ItemSlot>();
            slots.Add(slot);
        }
    }
}
