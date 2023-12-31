using UnityEngine;

public class ItemDataSO : ScriptableObject {
    [field: SerializeField] public string label { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
}