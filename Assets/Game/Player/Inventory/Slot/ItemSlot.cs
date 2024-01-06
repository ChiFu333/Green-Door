using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    [SerializeField] private Image itemImage;
    private Item item;
    #region Interaction
    public void SetItem(Item _item)
    {
        item = _item;
        UpdateDisplay();   
    }
    public void RemoveItem() {
        item = null;
        UpdateDisplay();
    }
    #endregion
    #region Getters
    public bool IsFree() => item == null;
    public Item GetItem() => item;
    #endregion
    private void UpdateDisplay() {
        if (item == null) { 
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            return;
        }
        itemImage.sprite = item.data.icon;
        itemImage.color = Color.white;
    }
}