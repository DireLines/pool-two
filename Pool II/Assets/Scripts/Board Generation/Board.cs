using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    Bounds bounds;
    Transform wall;

    float thickness = 4f, padding = 0.9f;

    private void Awake()
    {
        var box = GetComponent<BoxCollider2D>();
        //box.size = GetComponentInChildren<SpriteRenderer>().bounds.size;
        bounds = box.bounds;
        wall = transform.FindDeepChild("Wall");
        //SetupWalls();
    }

    public void SetupWalls()
    {
        var box = GetComponent<BoxCollider2D>();
        box.size = GetComponentInChildren<SpriteRenderer>().bounds.size;
        bounds = box.bounds;
        Vector2 scale = box.size;
        scale.Scale(new Vector2(0.9f, 0.9f));
        box.size = scale;
        wall = transform.FindDeepChild("Wall");
        BoxCollider2D[] walls = wall.GetComponentsInChildren<BoxCollider2D>();
        walls[0].offset = new Vector2(0, bounds.extents.y + thickness);
        walls[0].size = new Vector2(bounds.size.x, thickness*2);

        walls[1].offset = new Vector2(bounds.extents.x + thickness, 0);
        walls[1].size = new Vector2(thickness*2, bounds.size.y);

        walls[2].offset = new Vector2(0, -bounds.extents.y - thickness);
        walls[2].size = new Vector2(bounds.size.x, thickness*2);

        walls[3].offset = new Vector2(-bounds.extents.x - thickness, 0);
        walls[3].size = new Vector2(thickness*2, bounds.size.y);
    }

    public void DeactivateWalls()
    {
        wall.gameObject.SetActive(false);
    }


    public Vector2 RandomPointInBounds()
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
