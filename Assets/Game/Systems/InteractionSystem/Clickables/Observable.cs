using UnityEngine;

public class Observable : ClickableThing {
    [SerializeField] private Phrase phrase;
    public override void HandleInteraction() {
        base.HandleInteraction();
        DialogueSystem.inst.SayPhrase(phrase);
    }
}
