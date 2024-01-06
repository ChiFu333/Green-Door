using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Dialogue/Character")]
public class CharacterSO : ScriptableObject {
    [field: SerializeField] public Sprite face { get; private set; }
    [field: SerializeField] public bool onRightSide { get; private set; }

    [field: SerializeField] public string label { get; private set; }
    [field: SerializeField] public Color textColor { get; private set; }
    [field: SerializeField] public AudioClip voice { get; private set; }
}
