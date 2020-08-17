using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomRotateSetting { Flip, Turn, None };

public class RandomTiles : Affecter
{
    public List<Sprite> tiles = new List<Sprite>();
    public RandomRotateSetting rotateSetting;
    public bool randomXFlip, randomYFlip, applyToSprites;

    public void Setup()
    {
        if (tiles.Count > 0) Apply<SpriteRenderer>(ApplyTile);
        if (!applyToSprites) Apply<Transform>(ApplyFlip);
    }

    void ApplyTile(SpriteRenderer target)
    {
        target.sprite = tiles[Random.Range(0, tiles.Count)];
        if (applyToSprites) ApplyFlip(target.transform);
    }

    void ApplyFlip(Transform target)
    {
        Transform parent = target.parent;
        target.parent = null;
        switch (rotateSetting)
        {
            case RandomRotateSetting.Flip:
                transform.Rotate(0, 0, 180 * (int)Random.Range(0, 1));
                break;
            case RandomRotateSetting.Turn:
                transform.Rotate(0, 0, 90 * (int)Random.Range(0, 4));
                break;
            case RandomRotateSetting.None:
                break;
        }
        target.localScale = new Vector3(
            target.localScale.x * ((randomXFlip) ? ExtensionMethods.RandomSign() : 1),
            target.localScale.y * ((randomYFlip) ? ExtensionMethods.RandomSign() : 1),
            1);
        target.parent = parent;
    }
}
