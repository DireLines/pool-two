using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTextHandler : MonoBehaviour {
    [SerializeField]
    private TMP_Text text = null;


    private void Update() {
        EconomyManager EM = TurnManager.instance.currentPlayer().wallet;
        UpdateGoldText(EM.currentGold);
    }

    public void UpdateGoldText(int gold) {
        text.text = gold.ToString();
    }
}
