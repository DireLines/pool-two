using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUIManager : MonoBehaviour
{
    public static ScoreUIManager instance;

    public List<Image> playerCursors;
    public List<TMP_Text> playerScores;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetScore(int playerNumber, int score)
    {
        if (playerNumber > playerScores.Count) return;
        playerScores[playerNumber].text = $"P{playerNumber+1} - {score.ToString()}";
    }

    public void SetActiveCursor(int playerNumber)
    {
        if (playerNumber > playerCursors.Count) return;
        playerCursors[(playerNumber + 1) % 2].gameObject.SetActive(false);
        playerCursors[playerNumber].gameObject.SetActive(true);
    }
}
