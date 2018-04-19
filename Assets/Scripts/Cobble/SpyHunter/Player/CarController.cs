using System;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class CarController : MonoBehaviour {
        [Serializable]
        public class CarMovementSettings {
            [Tooltip("The speed that the character moves at")]
            public float MovementSpeed = 5.0f;

            public float HorizontalSpeed = 0.5f;

            public float MaxSpeed = 25.0f;

            public float CurrentTargetSpeed;
        }

        public CarMovementSettings MovementSettings = new CarMovementSettings();

        [SerializeField] private Rigidbody _rigidbody;

        private void Start() {
            if (!_rigidbody)
                _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        private void FixedUpdate() {
            var input = GetInput();
            if (_rigidbody.velocity.sqrMagnitude <
                MovementSettings.CurrentTargetSpeed * MovementSettings.CurrentTargetSpeed) {
                _rigidbody.AddForce(transform.forward * MovementSettings.MovementSpeed, ForceMode.VelocityChange);
            }

            if (Math.Abs(input.x) > float.Epsilon && MovementSettings.CurrentTargetSpeed > 1f)
                _rigidbody.MovePosition(_rigidbody.position + transform.right * input.x * MovementSettings.HorizontalSpeed);
//				_rigidbody.AddForce(transform.right * input.x * MovementSettings.HorizontalSpeed, ForceMode.Force);
        }

        private Vector2 GetInput() {
            var input = new Vector2 {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
            UpdateCurrentSpeed(input);
            return input;
        }

        private void UpdateCurrentSpeed(Vector2 input) {
            if (input == Vector2.zero) return;
            MovementSettings.CurrentTargetSpeed = Mathf.Clamp(MovementSettings.CurrentTargetSpeed + input.y, 0f,
                MovementSettings.MaxSpeed);
        }
    }
}