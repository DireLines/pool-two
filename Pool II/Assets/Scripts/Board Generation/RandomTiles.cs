using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomTileSetting { Self, Children, All, WithName, None};
public enum RandomRotateSetting { Flip, Turn, None };
public delegate void ApplyDel<T>(T obj);

public class RandomTiles : MonoBehaviour
{
    public List<Sprite> tiles = new List<Sprite>();
    public RandomTileSetting tileSetting;
    public string targetName;
    public RandomRotateSetting rotateSetting;
    public bool randomXFlip, randomYFlip, applyToSprites;

    public void Setup()
    {
        if (tiles.Count > 0) Apply<SpriteRenderer>(ApplyTile);
        if (!applyToSprites) Apply<Transform>(ApplyFlip);
    }

    void Apply<T>(ApplyDel<T> application) where T : Component
    {
        List<T> targets = new List<T>();
        switch (tileSetting)
        {
            case RandomTileSetting.Self:
                targets.Add(GetComponent<T>());
                break;
            case RandomTileSetting.Children:
                targets.AddRange(gameObject.FindComponents<T>(in_parent: false, ignore_self: true));
                break;
            case RandomTileSetting.All:
                targets.AddRange(GetComponentsInChildren<T>());
                break;
            case RandomTileSetting.WithName:
                foreach (var child in transform.FindDeepChildren(targetName))
                    targets.Add(child.GetComponent<T>());
                break;
            case RandomTileSetting.None:
                break;
        }
        foreach (var target in targets)
            application(target);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
