﻿using System.Collections;
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
            FillUIForShopItem(item);
        }
    }

    GameObject FillUIForShopItem(ShopItem item) {
        GameObject itemUI = Instantiate(ShopItemUI, transform.FindDeepChild("Content"));
        //itemUI.GetComponent<Button>().onClick needs to trigger TryToPurchase on the item
        itemUI.GetComponent<Image>().sprite = item.icon;
        itemUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.name.ToUpper();
        itemUI.transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = "COST: " + item.cost;
        return itemUI;
    }
}