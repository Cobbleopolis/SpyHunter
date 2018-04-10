using UnityEngine;

namespace Cobble.SpyHunter.Player {
    
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerScroller : MonoBehaviour {
        
        public float Speed = 5f;

        public Vector3 ScrollDirection = Vector3.forward;

        public bool RelativeDirection;

        public ForceMode ScrollForceMode = ForceMode.Force;

        [SerializeField]
        private Rigidbody _rigidbody;
        
        private void Start() {
            if (!_rigidbody)
                _rigidbody = GetComponent<Rigidbody>();
            ScrollDirection = ScrollDirection.normalized * Speed;
        }

        private void FixedUpdate() {
            if (RelativeDirection)
                _rigidbody.AddRelativeForce(ScrollDirection, ScrollForceMode);
            else
                _rigidbody.AddForce(ScrollDirection, ScrollForceMode);
        }
    }
}