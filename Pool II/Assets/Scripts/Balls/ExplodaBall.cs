using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodaBall : BaseBall
{
    [SerializeField]
    protected float radius = 5.0F;
    [SerializeField]
    protected float power = 10.0F;

    bool fuse_lit = false;

    [SerializeField]
    Sprite lit_icon;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void OnHitByOtherBall()
    {
        fuse_lit = true;

        icon.sprite = lit_icon;

        print("shit's lit");
    }

    protected override void OnHitOtherBall()
    {
        if (fuse_lit)
        {
            Explode();
        }
    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        FX_Spawner.instance.SpawnFX(FXType.SmallExplosion, transform.position, Quaternion.identity, parent : transform);
        Destroy(gameObject);
    }

}
