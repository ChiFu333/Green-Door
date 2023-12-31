using UnityEngine;

public class ClickableItem : ClickableThing {
    [SerializeField] private ItemDataSO itemData;

    public override void HandleClick() {
        base.HandleClick();
        Player.inst.controller.MoveTo(transform.position, () => {
            PlayerInventory.inst.PickupItem(new Item(itemData));
        });
    }
}