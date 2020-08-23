using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBall : BaseBall {

    [SerializeField]
    GameObject flame_eyes;

    [SerializeField]
    GameObject bones_pile;

    bool kill_primed;

    protected override void OnHitByOtherBall(GameObject other)
    {
        kill_primed = true;
        flame_eyes.SetActive(true);
    }

    protected override void OnHitOtherBall(GameObject other)
    {
        // TODO(Simon): Don't kill your own balls
        if (kill_primed)
        {
            Instantiate(bones_pile, transform.position, Quaternion.identity);
            Instantiate(bones_pile, other.transform.position, Quaternion.identity);

            Destroy(other);
        }
    }

    protected override void OnSettle()
    {
        if (kill_primed)
        {
            Destroy(gameObject);
        }

        kill_primed = false;
        flame_eyes.SetActive(false);

    }
}
