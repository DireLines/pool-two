using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldTextHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text = null;

    [SerializeField]
    private EconomyManager economyManager = null;

    private void Update()
    {
        UpdateGoldText(economyManager.currentGold);
    }

    public void UpdateGoldText(int gold)
    {
        text.text = gold.ToString();
    }
}
