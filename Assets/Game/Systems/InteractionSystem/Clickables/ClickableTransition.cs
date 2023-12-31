using UnityEngine;

public class ClickableTransition : ClickableThing {
    [SerializeField] private string targetScene;
    [SerializeField] private Transform targetPosition;
    public override void HandleClick() {
        base.HandleClick();
        if (targetPosition == null) targetPosition = transform;
        Player.inst.controller.MoveTo(targetPosition.position, () => {
            SceneLoader.inst.LoadScene(targetScene);
        });
    }
}