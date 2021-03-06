﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [HideInInspector]
    public Shaker shaker;
    List<SpriteRenderer> renderers = new List<SpriteRenderer>();
    bool destroyed;
    float destroy_timer = 0.5f;

    public FXType destroyFX;

    public GameEvent DestroyEvent;

    // Start is called before the first frame update
    void Awake()
    {
        renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        shaker = gameObject.AddComponent<Shaker>();
        shaker.affecterName = "DestroyerShaker";
        shaker.affecterSetting = AffecterSetting.Self;
        shaker.speed = 20f;
    }

    public void Destroy()
    {
        if (destroyed) return;
        if (destroyFX != FXType.Default) FX_Spawner.instance.SpawnFX(destroyFX, transform.position, Quaternion.identity);
        DestroyEvent?.Invoke();
        StartCoroutine(DestroyCo());
    }

    IEnumerator DestroyCo()
    {
        destroyed = true;
        shaker.Activate(destroy_timer);
        float timer = destroy_timer;
        while (timer > 0)
        {
            foreach (var r in renderers)
            {
                r.color = Color.Lerp(
                    new Color(r.color.r, r.color.g, r.color.b, 1), 
                    new Color(r.color.r, r.color.g, r.color.b, 0), 
                    (destroy_timer - timer) / destroy_timer);
            }
            timer -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
