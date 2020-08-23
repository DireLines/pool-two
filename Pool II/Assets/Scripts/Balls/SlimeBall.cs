using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : BaseBall {

    [SerializeField]
    int blob_count;

    [SerializeField]
    float force;

    float radius;

    [SerializeField]
    GameObject slime_blob;

    protected void Awake()
    {
        if (!slime_blob)
            throw new System.Exception("SlimeBall MUST have a reference to a slime_blob GameObject");
    }

    protected override void Start()
    {
        base.Start();
        foreach (var collider in GetComponents<CircleCollider2D>())
            if (collider.isTrigger) radius = collider.radius;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnHitByOtherBall()
    {
        Explode();
    }

    protected void Explode()
    {
        for (int i=0; i<blob_count; i++)
        {
            GameObject blobbo = Instantiate(slime_blob, transform.position, Quaternion.identity);

            Rigidbody2D blobbo_rb = blobbo.GetComponent<Rigidbody2D>();

            Vector2 force_vector = (Random.insideUnitCircle * radius).normalized * force;
            blobbo_rb.AddForce(force_vector, ForceMode2D.Impulse);
        }
        FX_Spawner.instance.SpawnFX(FXType.SlimePop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
