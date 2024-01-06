using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObservableItem : ClickableThing {
    [SerializeField] private List<ItemDataSO> requirements = new List<ItemDataSO>();
    [SerializeField] private Phrase phrase;
    [Header("Item")]
    [SerializeField] private ItemDataSO itemData;
    [SerializeField] private AudioClip itemTaked;
    [SerializeField] private UnityEvent AfterUseCallback = new UnityEvent();
    public override void HandleClick() {
        base.HandleClick();
        if (IsSatisfied()) {
            Player.inst.controller.MoveTo(transform.position, () => {
                AudioManager.inst.Play(new AudioQuery(itemTaked));
                PlayerInventory.inst.RemoveItem(requirements[0]); // aaaaa
                if (itemData != null)
                {
                    PlayerInventory.inst.PickupItem(new Item(itemData));
                }
                AfterUseCallback.Invoke();
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
