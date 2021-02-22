using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        Sphere,
        Square,
        Rectangle,
    }

    public ItemType itemType;

    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sphere:    return ItemAssets.Instance.SphereSprite;
            case ItemType.Square:    return ItemAssets.Instance.SquareSprite;
            case ItemType.Rectangle: return ItemAssets.Instance.RectangleSprite;
        }
    }
}
