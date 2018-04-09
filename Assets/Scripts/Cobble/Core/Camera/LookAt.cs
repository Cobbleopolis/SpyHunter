using UnityEngine;

namespace Cobble.Core.Camera {
	public class LookAt : MonoBehaviour {

		[Tooltip("The tranform to look at.")]
		public Transform Target;

		[Tooltip("If true, the object will be set to look at the target every Update(). Otherwise, it is only set on Start().")]
		public bool ConstantUpdate = true;

		private void Start () {
			transform.LookAt(Target);
		}
	
		private void Update () {
			if (ConstantUpdate)
				transform.LookAt(Target);
		}
	}
}
