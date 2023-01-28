using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DHToolbox.Runtime.CharacterControllers
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerBasedMotor : BaseMotor
    {
        [SerializeField] private CharacterController characterController;

        protected void OnValidate()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        protected override void Move(Vector3 direction)
        {
            characterController.SimpleMove(direction.normalized * speedProvider.Speed);
        }

        protected override IObservable<Unit> Update => this.UpdateAsObservable();
    }
}