using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EconomyManager : MonoBehaviour {
    [SerializeField]
    [Range(0, 5000)]
    private int startingGold = 1000;

    [HideInInspector]
    public int currentGold { get; private set; }

    [SerializeField]
    private ShopItemList allItems = null;
    private List<ShopItem> items;

    void Awake() {
        items = new List<ShopItem>(allItems.shopItems);
        currentGold = startingGold;
    }

    public List<ShopItem> GetShopItems() => items;

    public int GetCurrentGold() => currentGold;

    public bool TryPurchaseItem(ShopItem item) {
        int itemIndex = items.IndexOf(item);
        if (itemIndex < 0 || itemIndex >= items.Count) return false;
        if (item.cost > currentGold) return false;
        currentGold -= item.cost;
        return true;
    }

    public void Add(int amount) {
        currentGold += amount;
    }
}
