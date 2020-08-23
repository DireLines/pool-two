using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBall : BaseBall
{
    List<Rigidbody2D> targets = new List<Rigidbody2D>();
    public GameObject joint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.HasTag(Tag.Ball))
        {
            AttachToTarget(collision.gameObject.GetComponent<Rigidbody2D>());
        }
    }

    void AttachToTarget(Rigidbody2D r)
    {
        if (!targets.Contains(r))
        {
            print("goo goo");
            targets.Add(r);
            Sticker sticker = Instantiate(joint, transform).GetComponent<Sticker>();
            print(sticker);
            sticker.AddTarget(r);
            sticker.OnBreakEvent += Deregister;
        }
    }

    void Deregister(Rigidbody2D r)
    {
        targets.Remove(r);
    }
}
