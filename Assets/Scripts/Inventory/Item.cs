using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Banana,
        Chapeu,
        Arvore,
        Moeda,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Banana: return ItemAssets.Instance.swordSprite;
            case ItemType.Chapeu: return ItemAssets.Instance.keySprite;
            case ItemType.Arvore: return ItemAssets.Instance.treeSprite;
            case ItemType.Moeda: return ItemAssets.Instance.coinSprite;

        }
    }

    public Item(Item.ItemType type, int amount)
    {
        this.itemType = type;
        this.amount = amount;
    }
}
