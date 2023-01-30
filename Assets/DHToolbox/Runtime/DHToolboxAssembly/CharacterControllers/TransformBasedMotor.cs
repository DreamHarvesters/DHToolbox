using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.CharacterControllers
{
    public class TransformBasedMotor : BaseMotor
    {
        protected override void Move(Vector3 direction)
        {
            transform.position += (direction * speedProvider.Speed * Time.deltaTime);
        }

        protected override IObservable<Unit> Update => this.UpdateAsObservable();
    }
}