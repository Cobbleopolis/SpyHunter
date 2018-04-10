using System;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
	
	public class CarController : MonoBehaviour {

		[Serializable]
		public class CarMovementSettings {
			[Tooltip("The speed that the character moves at")]
			public float MovementSpeed = 5.0f;

			public float MaxSpeed = 25.0f;

			public float CurrentSpeed;
		
			public float CurrentTargetSpeed;
		}

		[Serializable]
		public class CarAdvancedSettings {
			[Tooltip("The distance for checking if the controller is grounded (0.01f seems to work best for this)")]
			public float GroundCheckDistance = 0.01f;
			public LayerMask GroundCheckLayerMask = -1;
			public float StickToGroundHelperDistance = 0.5f; // stops the character
			[Tooltip("Can the user control the direction that is being moved in the air")]
			public bool AirControl; // 
			[Tooltip("Reduce the radius by that ratio to avoid getting stuck in wall. Set it to 0.1 or more if you get stuck in wall")]
			public float ShellOffset;
		}

		public CarMovementSettings MovementSettings = new CarMovementSettings();
		public CarAdvancedSettings AdvancedSettings = new CarAdvancedSettings();

		[SerializeField]
		private Rigidbody _rigidbody;

		[SerializeField] 
		private BoxCollider _boxCollider;
		
		private Vector3 _groundContactNormal;
		private bool _isGrounded;

		private void Start () {
			if (!_rigidbody)
				_rigidbody = GetComponentInChildren<Rigidbody>();
			if (!_boxCollider)
				_boxCollider = GetComponentInChildren<BoxCollider>();
		}
	
		private void FixedUpdate() {
			GroundCheck();
			var input = GetInput();
			Debug.DrawRay(_rigidbody.position + new Vector3(1f, 0f, 0f), Quaternion.AngleAxis(90, Vector3.right) * new Vector3(input.x, input.y, 0f), Color.red, 0f, false);
			Debug.DrawRay(_rigidbody.position, _rigidbody.velocity, Color.green, 0f, false);
			if (_rigidbody.velocity.sqrMagnitude <
			    MovementSettings.MaxSpeed * MovementSettings.MaxSpeed)
				_rigidbody.AddForce(transform.forward * input.y * MovementSettings.MovementSpeed, ForceMode.Impulse);
			_rigidbody.AddForce(transform.right * input.x * MovementSettings.MovementSpeed, ForceMode.VelocityChange);
		}

		private Vector2 GetInput() {
			var input = new Vector2 {
				x = Input.GetAxis("Horizontal"),
				y = Input.GetAxis("Vertical")
			};
//			UpdateCurrentSpeed(input);
			return input;
		}

//		private void UpdateCurrentSpeed(Vector2 input) {
//			if (input == Vector2.zero) return;
//			MovementSettings.CurrentTargetSpeed = Mathf.Clamp(MovementSettings.CurrentTargetSpeed + input.y, 0f, MovementSettings.MaxSpeed);
//		}

		private void GroundCheck() {
			RaycastHit raycastHit;
			if (Physics.BoxCast(_rigidbody.position, _boxCollider.size / 2f, -transform.up, out raycastHit, transform.rotation,
				AdvancedSettings.GroundCheckDistance, AdvancedSettings.GroundCheckLayerMask.value,
				QueryTriggerInteraction.Ignore)) {
				_isGrounded = true;
				_groundContactNormal = raycastHit.normal;
			} else {
				_isGrounded = false;
				_groundContactNormal = Vector3.up;
			}
		}
	}
}
