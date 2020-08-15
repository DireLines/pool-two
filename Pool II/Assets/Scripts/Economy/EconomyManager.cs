using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EconomyManager : MonoBehaviour {
    [SerializeField]
    [Range(0, 5000)]
    private int startingGold = 1000;
    
    [HideInInspector]
    public int currentGold { get; private set; }

    [SerializeField]
    private ShopItemList specialItemList = null;
    [SerializeField]
    private List<ShopItem> items;

    string PrefabsFolderAbsolutePath;

    void Awake() {

        items = new List<ShopItem>(specialItemList.shopItems);
        string prefabsFolder = Path.Combine(Application.dataPath, "Prefabs");
        string[] prefabPaths = Directory.GetFiles(prefabsFolder, "*.prefab", SearchOption.AllDirectories);
        foreach (string path in prefabPaths) {
            print(path);
            string assetPath = "Assets" + path.Replace(Application.dataPath, "").Replace("\\", "/");
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
            items.Add(ShopItemFor(prefab));
        }

        currentGold = startingGold;
    }

    public List<ShopItem> GetShopItems() {
        return items;
    }

    public int GetCurrentGold() => currentGold;

    public bool TryPurchaseItem(int itemIndex, Vector3 spawnPoint) {
        if (itemIndex < 0 || itemIndex >= items.Count) return false;
        ShopItem item = items[itemIndex];
        if (item.cost > currentGold) return false;
        currentGold -= item.cost;
        Instantiate(item.prefab, spawnPoint, Quaternion.identity);
        return true;
    }

    ShopItem ShopItemFor(GameObject prefab) {
        ShopItem result = new ShopItem
        {
            name = prefab.name,
            cost = prefab.GetComponent<BaseBall>().cost,
            prefab = prefab,
            icon = prefab.transform.FindDeepChild("Sprite").GetComponent<SpriteRenderer>().sprite,
        };
        return result;
    }
}
