using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class PopulateShopItems : MonoBehaviour {
    string PrefabsFolderAbsolutePath;
    void Awake() {
        PrefabsFolderAbsolutePath = Path.Combine(Application.dataPath, "Prefabs");
        string[] absolutePaths = Directory.GetFiles(PrefabsFolderAbsolutePath, "*.prefab", SearchOption.AllDirectories);
        foreach (string path in absolutePaths) {
            print(path);
            string assetPath = "Assets" + path.Replace(Application.dataPath, "").Replace("\\", "/");
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject));
            ShopItem shopItem = ShopItemFor(prefab);
        }
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
