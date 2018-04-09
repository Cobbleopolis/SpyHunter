using System;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
	
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CharacterController))]
	public class CarController : MonoBehaviour {

		[Tooltip("The speed that the character moves at")]
		public float Speed = 5;

		[SerializeField]
		private CharacterController _characterController;

		private void Start () {
			if (!_characterController)
				_characterController = GetComponent<CharacterController>();
		}
	
		private void Update () {
			_characterController.SimpleMove(Speed * Input.GetAxis("Vertical") * transform.forward);
		}
	}
}
