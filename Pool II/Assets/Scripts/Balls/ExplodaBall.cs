using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodaBall : BaseBall
{
    [SerializeField]
    protected float power = 10.0F;

    float radius;

    bool fuse_lit = false;

    [SerializeField]
    Sprite lit_icon;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foreach (var collider in GetComponents<CircleCollider2D>())
            if (collider.isTrigger) { radius = collider.radius; collider.enabled = false; }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void OnHitByOtherBall(GameObject other)
    {
        fuse_lit = true;

        icon.sprite = lit_icon;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (fuse_lit)
        {
            Explode();
        }

        base.OnCollisionEnter2D(collision);
    }

    void Explode()
    {
        // TODO: destroy objects around you
        // TODO: add sound effect to Big & Small Explosion prefabs

        Vector3 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.transform.parent && hit.transform.parent.gameObject.HasTag(Tag.Feature) && hit.GetComponentInParent<Destroyer>())
                hit.GetComponentInParent<Destroyer>().Destroy();
            Rigidbody2D r = hit.GetComponent<Rigidbody2D>();

            if (r != null)
                r.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        FX_Spawner.instance.SpawnFX(FXType.SmallExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
