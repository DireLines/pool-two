using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopDisplay : MonoBehaviour {
    [SerializeField]
    EconomyManager EM;
    [SerializeField]
    GameObject ShopItemUI;

    void Start() {
        foreach (ShopItem item in EM.GetShopItems()) {
            print(item.name);
            UIForShopItem(item);
        }
    }

    GameObject UIForShopItem(ShopItem item) {
        GameObject itemUI = Instantiate(ShopItemUI, transform.FindDeepChild("Content"));
        itemUI.GetComponent<Image>().sprite = item.icon;
        itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name.ToUpper();
        return itemUI;
    }
}
