using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemCombination", menuName = "Items/ItemCombination")]
public class ItemCombinationSO : ScriptableObject {
    [field: SerializeField] public ItemDataSO firstObject { get; private set; }
    [field:SerializeField] public ItemDataSO secondObject { get; private set; }
    [field:SerializeField] public ItemDataSO resultingObject { get; private set; }

    public bool IsItemRequired(ItemDataSO data) {
        return firstObject == data || secondObject == data;
    }

    public bool IsEnoughItems(List<ItemDataSO> items) {
        bool firstFound = false, secondFound = false;
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == firstObject) { firstFound = true; continue; }
            if (items[i] == secondObject) { secondFound = true; continue; }
        }
        return firstFound && secondFound;
    }
}
