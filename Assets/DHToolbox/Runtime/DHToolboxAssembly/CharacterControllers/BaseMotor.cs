using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CharacterControllers
{
    public abstract class BaseMotor : MonoBehaviour
    {
        [SerializeField] private bool lookForward;
        [SerializeField] private DirectionProvider directionProvider;
        [SerializeField] protected SpeedProvider speedProvider;

        // protected Joystick Joystick
        // {
        //     get
        //     {
        //         if (joystick == null)
        //             joystick = FindObjectOfType<Joystick>();
        //
        //         return joystick;
        //     }
        // }
        //
        // protected virtual void OnValidate()
        // {
        //     if (joystick == null)
        //         joystick = FindObjectOfType<Joystick>();
        // }

        protected void Start()
        {
            Update
                .Where(_ => enabled)
                .Subscribe(_ => Move(directionProvider.Direction))
                .AddTo(gameObject);

            if (lookForward)
                this.UpdateAsObservable()
                    .Where(_ => enabled)
                    .Select(_ => directionProvider.Direction)
                    .Where(direction => direction.sqrMagnitude != 0)
                    .Subscribe(direction =>
                        transform.rotation = Quaternion.LookRotation(direction)).AddTo(gameObject);
        }

        protected abstract IObservable<Unit> Update { get; }

        protected abstract void Move(Vector3 direction);
    }
}