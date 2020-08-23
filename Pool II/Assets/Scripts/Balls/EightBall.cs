using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightBall : BaseBall {
    protected override void OnHitByOtherBall(GameObject other) {
        print("OnHitByOtherBall");
    }
    protected override void OnHitOtherBall(GameObject other) {
        print("OnHitOtherBall");
    }
    protected override void OnMoving() {
        print("OnMoving");
    }
    protected override void OnSettle() {
        print("OnSettle");
    }
    public override void OnSink() {
        print("OnSink");
    }
}
