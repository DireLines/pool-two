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
        base.OnHitByOtherBall();
        fuse_lit = true;
    }

    protected override void OnHitOtherBall()
    {
        base.OnHitOtherBall();

        if (fuse_lit)
        {
            Explode();
        }
    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }

}
