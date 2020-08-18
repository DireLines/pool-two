using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Feature : MonoBehaviour
{

    protected Transform simulatorBody, body, sprite;
    protected Rigidbody2D rb;

    protected Destroyer destroyer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    public virtual void CloneSetup()
    {

    }

    public virtual void Setup()
    {
        foreach (var randomTile in GetComponentsInChildren<RandomTiles>()) randomTile.Setup();
        simulatorBody = transform.FindDeepChild("SimulatorBody");
        body = transform.FindDeepChild("Body");
        body.gameObject.SetActive(false);
        sprite = transform.FindDeepChild("Sprite");
        sprite.gameObject.SetActive(false);
    }

    public virtual void PostSetup()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        body = transform.FindDeepChild("Body");
        sprite = transform.FindDeepChild("Sprite");
        destroyer = GetComponent<Destroyer>();
        TagHandler handler = GetComponent<TagHandler>();
        if (!GetComponent<TagHandler>())
            handler = gameObject.AddComponent<TagHandler>();
        handler.tags.Add(Tag.Feature);
        if (destroyer) destroyer.DestroyEvent += Destroy;
    }

    protected virtual void Destroy()
    {
    }

    public virtual void Place()
    {
        Vector2 pos = simulatorBody.position;
        transform.position = pos;
        //sprite.position = simulatorBody.position;
        sprite.gameObject.SetActive(true);
        //body.position = simulatorBody.position;
        body.gameObject.SetActive(true);
        Destroy(simulatorBody.gameObject);
    }
}
