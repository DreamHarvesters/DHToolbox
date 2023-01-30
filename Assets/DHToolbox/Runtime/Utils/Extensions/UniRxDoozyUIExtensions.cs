namespace DHToolbox.Runtime.Utils.Extensions
{
#if DOOZY
    using Doozy.Runtime.Signals;
    using Doozy.Runtime.UIManager.Components;
    using Doozy.Runtime.UIManager.Containers;

    public static class UniRxDoozyUIExtensions
    {
        public static IObservable<Unit> OnVisibleObservable(this UIContainer view)
        {
            return view.OnVisibleCallback.Event.AsObservable();
        }

        public static IObservable<Unit> OnShowObservable(this UIContainer view)
        {
            return view.OnShowCallback.Event.AsObservable();
        }

        public static IObservable<Unit> OnHiddenObservable(this UIContainer view)
        {
            return view.OnHiddenCallback.Event.AsObservable();
        }

        public static IObservable<Unit> OnHideObservable(this UIContainer view)
        {
            return view.OnHideCallback.Event.AsObservable();
        }

        public static IObservable<UIButton> OnClickedAsObservable(this UIButton button) =>
            button.onClickEvent.AsObservable().Select(_ => button);

        public static IObservable<Signal> OnSignalAsObservable(this SignalStream stream) =>
            Observable.FromEvent<UnityAction<Signal>, Signal>(action => action.Invoke,
                signal => UIButton.stream.OnSignal += signal,
                action => UIButton.stream.OnSignal -= action);
    }
#endif
}