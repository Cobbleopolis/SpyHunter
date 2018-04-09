using UnityEngine;

namespace Cobble.Core.Camera {
    public class FollowCamera : MonoBehaviour {

		public Transform Target;
        
        public Vector3 CameraOffset = new Vector3(0, 10, -30);
        
	    [SerializeField]
        private bool _lockBehind = true;

	    private Transform _rotateTransform;

		private void Start() {
			_updatePos();
		}

		private void Update() {
			_updatePos();            
        }

	    private void _updatePos() {
		    if (!Target) return;
		    var updatedPos = Target.position + CameraOffset;
		    if (_lockBehind)
			    updatedPos = Quaternion.AngleAxis(Target.rotation.y, Target.up) * updatedPos;
		    transform.position = updatedPos;
	    }
    }
}