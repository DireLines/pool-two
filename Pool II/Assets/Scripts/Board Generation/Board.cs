using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    Bounds bounds;
    Transform wall;

    float thickness = 1f, padding = 0.75f;

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
        var parents = GetComponentsInParent<Transform>();
        Dictionary<Transform, Vector3> originalScale = new Dictionary<Transform, Vector3>();
        foreach (var parent in parents) 
        {
            originalScale[parent] = parent.localScale;
            parent.localScale = Vector3.one;
        }
        var box = GetComponent<BoxCollider2D>();
        box.size = GetComponent<SpriteRenderer>().bounds.size;
        bounds = box.bounds;
        Vector2 boxScale = box.size;
        boxScale.Scale(new Vector2(padding, padding));
        box.size = boxScale;
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
        foreach (var parent in parents)
        {
            parent.localScale = originalScale[parent];
        }
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
