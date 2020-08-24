using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBall : BaseBall {

    [SerializeField]
    GameObject flame_eyes;

    [SerializeField]
    GameObject bones_pile;

    bool kill_primed;

    protected override void OnHitByOtherBall(GameObject other, Collision2D collision)
    {
        kill_primed = true;
        flame_eyes.SetActive(true);
        FX_Spawner.instance.SpawnFX(FXType.FlamingBall, transform.position, transform.rotation.eulerAngles, parent : transform);
    }

    protected override void OnHitOtherBall(GameObject other, Collision2D collision)
    {
        // TODO(Simon): Don't kill your own balls
        if (kill_primed && !other.HasTag(Tag.Cue))
        {
            FX_Spawner.instance.SpawnFX(FXType.BallConflagration, other.transform.position, Quaternion.identity);
            Instantiate(bones_pile, other.transform.position, Quaternion.identity);
            Destroy(other);
        }
    }

    protected override void OnSettle()
    {
        if (kill_primed)
        {
            FX_Spawner.instance.SpawnFX(FXType.BallConflagration, transform.position, Quaternion.identity);
            Instantiate(bones_pile, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        kill_primed = false;
        flame_eyes.SetActive(false);

    }
}
