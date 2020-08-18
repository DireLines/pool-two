public class EightBall : BaseBall {
    public override void OnHitByOtherBall() {
        print("OnHitByOtherBall");
    }
    public override void OnHitOtherBall() {
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
