using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5000)]
    private int startingGold = 1000;
    private int currentGold;

    [SerializeField]
    private ShopItemList shopItemList = null;

    private void Start()
    {
        currentGold = startingGold;
    }

    public List<ShopItem> GetShopItems()
    {
        if (null == shopItemList) return new List<ShopItem>();
        return shopItemList.shopItems;
    }

    public int GetCurrentGold() => currentGold;

    public bool TryPurchaseItem(int itemIndex, Vector3 spawnPoint)
    {
        if (itemIndex < 0 || itemIndex >= shopItemList.shopItems.Count) return false;
        ShopItem item = shopItemList.shopItems[itemIndex];
        if (item.cost > currentGold) return false;
        currentGold -= item.cost;
        Instantiate(item.prefab, spawnPoint, Quaternion.identity);
        return true;
    }
}
