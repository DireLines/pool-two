using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBall : BaseBall {

    protected override void OnHitByOtherBall(GameObject other)
    {
        // TODO(Simon): Give skull ball firey eyes
    }

    protected override void OnHitOtherBall(GameObject other)
    {
        Destroy(other);
        Destroy(gameObject);
    }

}
