#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
public class PopulateShopItems : SaveAssetsProcessor {

    static string[] OnWillSaveAssets(string[] paths) {
        Debug.Log("OnWillSaveAssets called on:");
        foreach (string path in paths) {
            Debug.Log(path);
        }
        string specialItemsPath = "Assets/Special Shop Items.asset";
        string allItemsPath = "Assets/All Shop Items.asset".Replace("\\", "/");
        ShopItemList specialItems = (ShopItemList)AssetDatabase.LoadAssetAtPath(specialItemsPath, typeof(ShopItemList));
        ShopItemList allItems = (ShopItemList)AssetDatabase.LoadAssetAtPath(allItemsPath, typeof(ShopItemList));
        allItems.shopItems = new List<ShopItem>(specialItems.shopItems);

        string prefabsFolder = Path.Combine(Application.dataPath, "Prefabs", "Balls");
        string[] prefabPaths = Directory.GetFiles(prefabsFolder, "*.prefab", SearchOption.AllDirectories);
        foreach (string path in prefabPaths) {
            string assetPath = "Assets" + path.Replace(Application.dataPath, "").Replace("\\", "/");
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
            allItems.shopItems.Add(ShopItemFor(prefab));
        }
        return paths;
    }
    static ShopItem ShopItemFor(GameObject prefab) {
        return new ShopItem {
            name = prefab.name,
            cost = prefab.GetComponent<BaseBall>().cost,
            prefab = prefab,
            icon = prefab.transform.FindDeepChild("Icon").GetComponent<SpriteRenderer>().sprite,
        };
    }
}
#endif