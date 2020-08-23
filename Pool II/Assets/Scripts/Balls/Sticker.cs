using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RBEvent(Rigidbody2D rigidbody);
[RequireComponent(typeof(LineRenderer))]
public class Sticker : MonoBehaviour
{
    LineRenderer lr;
    SpringJoint2D joint;
    bool targetSet;
    Rigidbody2D target;
    public RBEvent OnBreakEvent;

    float breakForce = 25f;

    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        //joint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetSet) return;
        if (joint && target)
        {
            //joint.connectedBody.angularVelocity = 0;
            lr.SetPosition(0, transform.parent.position);
            lr.SetPosition(1, target.transform.position);
            return;
        }
        if (target)
        {
            print("BREAK");
            OnBreakEvent?.Invoke(target);
        }
        if (joint) Destroy(joint);
        Destroy(gameObject);
    }

    public void AddTarget(Rigidbody2D rigidbody)
    {
        target = rigidbody;
        if (!joint) { 
            joint = transform.parent.gameObject.AddComponent<SpringJoint2D>(); joint.breakForce = breakForce;
            joint.connectedBody = rigidbody;
            joint.autoConfigureDistance = false;
            joint.enableCollision = true;
            joint.distance = Vector2.Distance(rigidbody.transform.position, transform.position);
        }
        targetSet = true;
    }
}
