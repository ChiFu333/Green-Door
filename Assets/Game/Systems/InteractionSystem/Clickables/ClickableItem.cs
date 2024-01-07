using UnityEngine;
using UnityEngine.Events;

public class ClickableItem : ClickableThing {
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private AudioClip itemTaked;
    [SerializeField] private UnityEvent AfterUseCallback = new UnityEvent();
    public override void HandleClick() {
        base.HandleClick();
        Player.inst.controller.MoveTo(transform.position, () => {
            PlayerInventory.inst.PickupItem(new Item(itemData));
            AudioManager.inst.Play(new AudioQuery(itemTaked));
            FindObjectOfType<MagicOrchestrator>().items[key] = true;
            AfterUseCallback.Invoke();
            Destroy(gameObject);
        });
    }
}