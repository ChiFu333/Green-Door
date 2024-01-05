using UnityEngine;

public class ClickableItem : ClickableThing {
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private AudioClip itemTaked;

    public override void HandleClick() {
        base.HandleClick();
        Player.inst.controller.MoveTo(transform.position, () => {
            PlayerInventory.inst.PickupItem(new Item(itemData));
            FindObjectOfType<AudioManager>().Play(new AudioQuery(itemTaked));
            Destroy(gameObject);
        });
    }
}