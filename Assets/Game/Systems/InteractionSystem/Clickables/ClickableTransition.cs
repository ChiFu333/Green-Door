using UnityEngine;

public class ClickableTransition : ClickableThing {
    [SerializeField] private string targetScene;
    public override void HandleClick() {
        base.HandleClick();
    }
    public override void HandleInteraction() {
        base.HandleInteraction();
        SceneLoader.inst.LoadScene(targetScene);
    }
}