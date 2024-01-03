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
        int count = 0;
        for (int i = 0; i < items.Count; i++) {
            if (items[i] == firstObject) { count++; continue; }
            if (items[i] == secondObject) { count++; continue; }
        }
        return count == 2;
    }
}
