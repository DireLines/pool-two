using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplay : MonoBehaviour {
    [SerializeField]
    EconomyManager EM;
    void Start() {
        foreach (ShopItem item in EM.GetShopItems()) {
            print(item.name);
            //update UI to include button for item
        }
    }
}
