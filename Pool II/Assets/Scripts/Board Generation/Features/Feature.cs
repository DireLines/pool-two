using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{

    protected Transform simulatorBody, body, sprite;

    // Start is called before the first frame update
    protected virtual void Start()
    {
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
        sprite.position = simulatorBody.position;
        sprite.gameObject.SetActive(true);
        body.position = simulatorBody.position;
        body.gameObject.SetActive(true);
        Destroy(simulatorBody.gameObject);
    }
}
