namespace DHToolbox.Runtime.Utils.Extensions
{
#if LEAN_TOUCH
    using Lean.Touch;

    public static class LeanTouchExtensions
    {
        public static IObservable<LeanFinger> OnFingerDownAsObservable() =>
            Observable.FromEvent<LeanFinger>(action => LeanTouch.OnFingerDown += action,
                action => LeanTouch.OnFingerDown -= action);

        public static IObservable<LeanFinger> OnFingerTap() =>
            Observable.FromEvent<LeanFinger>(action => LeanTouch.OnFingerTap += action,
                action => LeanTouch.OnFingerTap -= action);

        public static IObservable<LeanFinger> OnFingerUpAsObservable() =>
            Observable.FromEvent<LeanFinger>(action => LeanTouch.OnFingerUp += action,
                action => LeanTouch.OnFingerUp -= action);

        public static IObservable<LeanFinger> OnFingerUpdateAsObservable() =>
            Observable.FromEvent<LeanFinger>(action => LeanTouch.OnFingerUpdate += action,
                action => LeanTouch.OnFingerUpdate -= action);
    }
#endif
}