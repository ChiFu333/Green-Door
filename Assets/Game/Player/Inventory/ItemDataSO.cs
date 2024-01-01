using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/ItemData")]
public class ItemDataSO : ScriptableObject {
    [field: SerializeField] public string label { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
}