using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservableItem : ClickableThing {
    [SerializeField] private List<ItemDataSO> requirements = new List<ItemDataSO>();
    [SerializeField] private Phrase phrase;
    [Header("Item")]
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private AudioClip itemTaked;
    public override void HandleClick() {
        base.HandleClick();
        if (IsSatisfied()) {
            Player.inst.controller.MoveTo(transform.position, () => {
                PlayerInventory.inst.PickupItem(new Item(itemData));
                AudioManager.inst.Play(new AudioQuery(itemTaked));
                Destroy(gameObject);
            });
        }
        
    }
    public override void HandleInteraction() {
        base.HandleInteraction();
        if (!IsSatisfied()) {
            DialogueSystem.inst.SayPhrase(phrase);
        }
    }

    private bool IsSatisfied() {
        List<ItemDataSO> datas = PlayerInventory.inst.GetItemDatas();
        for (int i = 0; i < requirements.Count; i++) {
            if (!datas.Contains(requirements[i])) return false;
        }
        return true;
    }
}
