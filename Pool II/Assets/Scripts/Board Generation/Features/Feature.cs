using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{

    protected Transform simulatorBody, body, sprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
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
