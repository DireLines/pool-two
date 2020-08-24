using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

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

    float max_distance = 2f, width_scale = 1f;

    public AnimationCurve width_curve;


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
            lr.startWidth = width_curve.Evaluate((Mathf.Clamp(Vector2.Distance(transform.parent.position, target.transform.position), 1, max_distance)-1) / (max_distance-1)) * width_scale;
            return;
        }
        if (target)
        {
            OnBreakEvent?.Invoke(target);
        }
        if (joint) Destroy(joint);
        Destroy(gameObject);
    }

    public void AddTarget(Rigidbody2D rigidbody)
    {
        FX_Spawner.instance.SpawnFX(FXType.WebStick, transform.position, Quaternion.identity);
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
