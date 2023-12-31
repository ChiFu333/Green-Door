using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
public class DialogueSO : ScriptableObject {
    [field: SerializeField] public List<Phrase> phrases { get; private set; } = new List<Phrase>();
}
