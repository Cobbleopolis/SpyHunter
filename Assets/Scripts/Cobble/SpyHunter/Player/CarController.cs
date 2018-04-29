using System;
using Cobble.Core.Lib.Ui;
using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class CarController : MonoBehaviour {
        [Serializable]
        public class CarMovementSettings {
            [Tooltip("The speed that the character moves at")]
            public float MovementSpeed = 5.0f;

            public float HorizontalSpeed = 0.5f;

            public float AccelerationSpeed = 20f;

            public float MaxSpeed = 25.0f;

            public float CurrentTargetSpeed;
        }

        public CarMovementSettings MovementSettings = new CarMovementSettings();

        public TrailRenderer AccelerationTrail;

        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private LifeHandler _lifeHandler;

        private void Start() {
            if (!_rigidbody)
                _rigidbody = GetComponentInChildren<Rigidbody>();
            if (!AccelerationTrail)
                AccelerationTrail = GetComponentInChildren<TrailRenderer>();
            if (AccelerationTrail)
                AccelerationTrail.enabled = false;
            
            if (!_lifeHandler)
                _lifeHandler = GetComponentInChildren<LifeHandler>();
        }

        private void Update() {
            if (GuiManager.Instance.CurrentGuiScreen == GuiScreen.None && Input.GetButtonDown("Pause"))
                GuiManager.Instance.Open(GuiScreen.PauseUi);
            else if (Input.GetButtonDown("Cancel") ||
                     GuiManager.Instance.CurrentGuiScreen == GuiScreen.PauseUi && Input.GetButtonDown("Pause"))
                GuiManager.Instance.GoBack();
        }

        private void FixedUpdate() {
            if (GameManager.Instance.IsPaused) return;
            var input = GetInput();
            if (_rigidbody.velocity.sqrMagnitude <
                MovementSettings.CurrentTargetSpeed * MovementSettings.CurrentTargetSpeed) {
                _rigidbody.AddForce(transform.forward * MovementSettings.MovementSpeed, ForceMode.VelocityChange);
            }

            if (Math.Abs(input.x) > float.Epsilon && MovementSettings.CurrentTargetSpeed > 1f)
                _rigidbody.MovePosition(_rigidbody.position +
                                        transform.right * input.x * MovementSettings.HorizontalSpeed);
            if (input.y > float.Epsilon)
                _rigidbody.AddForce(transform.forward * input.y * MovementSettings.AccelerationSpeed, ForceMode.Acceleration);
            if (AccelerationTrail)
                AccelerationTrail.enabled = input.y > float.Epsilon &&
                                            MovementSettings.CurrentTargetSpeed >= MovementSettings.MaxSpeed / 2f;
//            Debug.Log(_rigidbody.velocity);
        }

        private Vector2 GetInput() {
            if (GameManager.Instance.IsPaused) return Vector2.zero;
            var input = new Vector2 {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };
            UpdateCurrentSpeed(input);
            return input;
        }

        private void UpdateCurrentSpeed(Vector2 input) {
            if (input == Vector2.zero) return;
            if (!_lifeHandler.enabled && input.y > float.Epsilon)
                _lifeHandler.enabled = true;
            MovementSettings.CurrentTargetSpeed = Mathf.Clamp(MovementSettings.CurrentTargetSpeed + input.y, 0f,
                MovementSettings.MaxSpeed);
        }
    }
}