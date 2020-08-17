using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Feature : MonoBehaviour
{

    protected Transform simulatorBody, body, sprite;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.isKinematic = true;
        body = transform.FindDeepChild("Body");
        sprite = transform.FindDeepChild("Sprite");
    }

    public void Setup()
    {
        foreach (var randomTile in GetComponentsInChildren<RandomTiles>()) randomTile.Setup();
        simulatorBody = transform.FindDeepChild("SimulatorBody");
        body = transform.FindDeepChild("Body");
        body.gameObject.SetActive(false);
        sprite = transform.FindDeepChild("Sprite");
        sprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place()
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
