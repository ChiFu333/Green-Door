using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    public static PlayerInventory inst { get; private set; }
    private const int slotsAmount = 4;

    private Transform slotsHolder;
    private ItemSlot slotPrefab;
    private List<ItemCombinationSO> itemCombinations;
    private AudioQuery combinationQuery;

    private readonly List<ItemSlot> slots = new List<ItemSlot>();

    #region Managing
    public void Setup(Transform _slotsHolder, ItemSlot _slotPrefab, List<ItemCombinationSO> _itemCombinations, AudioQuery _combinationQuery) {
        slotsHolder = _slotsHolder;
        slotPrefab = _slotPrefab;
        itemCombinations = _itemCombinations;
        combinationQuery = _combinationQuery;
    }

    public void InitializeSlots() {
        for (int i = 0; i < slotsAmount; i++) {
            GameObject slotObject = Instantiate(slotPrefab.gameObject, slotsHolder, false);
            ItemSlot slot = slotObject.GetComponent<ItemSlot>();
            slots.Add(slot);
        }
    }

    public void SaveState() {
        GameManager.inst.playerItems = GetItems();
    }

    public void LoadState() {
        List<Item> items = GameManager.inst.playerItems;
        for (int i = 0; i < items.Count; i++) {
            if (items[i] != null) slots[i].SetItem(items[i]);
        }
    }
    #endregion

    public void PickupItem(Item item) {
        //Check if item can be combined with something else
        for (int i = 0; i < itemCombinations.Count; i++) {
            if (!itemCombinations[i].IsItemRequired(item.data)) continue;
            //Item is required, check if enough items for combination, using current items and item added
            List<ItemDataSO> itemDatas = GetItemDatas();
            itemDatas.Add(item.data);
            if (!itemCombinations[i].IsEnoughItems(itemDatas)) continue;
            //There is enough items, combine and do not add item to inventory
            ApplyCombination(itemCombinations[i]);
            return;
        }
        //Item cannot be combined, just add to inventory
        ItemSlot targetSlot = FindFreeSlot();
        targetSlot.SetItem(item);
    }

    public void RemoveItem(ItemDataSO itemData, bool removeAll = false) {
        for (int i = 0; i < slotsAmount; i++) {
            Item item = slots[i].GetItem();
            if (item == null) continue;
            if (item.data.Equals(itemData)) {
                slots[i].RemoveItem();
                if (!removeAll) return;
            }
        }
    }

    #region Internal
    private ItemSlot FindFreeSlot() {
        for (int i = 0; i < slotsAmount; i++) {
            if (slots[i].IsFree()) return slots[i];
        }
        throw new System.Exception("Not enough slots!");
    }

    private void ApplyCombination(ItemCombinationSO combination) {
        AudioManager.inst.Play(combinationQuery);
        RemoveItem(combination.firstObject);
        RemoveItem(combination.secondObject);
        PickupItem(new Item(combination.resultingObject));
    }

    private List<Item> GetItems() {
        List<Item> items = new List<Item>();
        for (int i = 0; i < slotsAmount; i++) {
            Item item = slots[i].GetItem();
            if (item != null) items.Add(item);
        }
        return items;
    }

    public List<ItemDataSO> GetItemDatas() {
        List<ItemDataSO> datas = new List<ItemDataSO>();
        for (int i = 0; i < slotsAmount; i++) {
            Item item = slots[i].GetItem();
            if (item != null) datas.Add(item.data);
        }
        return datas;
    }
    
    #endregion

    #region Initialization

    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
        }
    }
    #endregion
}
