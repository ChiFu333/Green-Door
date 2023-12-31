using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Phrase {
    [field: SerializeField] public CharacterSO character { get; private set; }
    [field: SerializeField, Multiline] public string text { get; private set; }
    [field: SerializeField] public UnityEvent callback { get; private set; } = new UnityEvent();
}
