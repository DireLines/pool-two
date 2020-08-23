using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBall : BaseBall {

    [SerializeField]
    int blob_count = 5;

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
            Vector3 relative_point = (Random.insideUnitCircle * radius);
            Vector3 world_point = transform.position + relative_point;

            Instantiate(slime_blob, world_point, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
