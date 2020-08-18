public class EightBall : BaseBall {
    protected override void OnHitByOtherBall() {
        print("OnHitByOtherBall");
    }
    protected override void OnHitOtherBall() {
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
