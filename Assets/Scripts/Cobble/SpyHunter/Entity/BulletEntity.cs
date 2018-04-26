using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    [RequireComponent(typeof(Rigidbody))]
    public class BulletEntity : MonoBehaviour {

        public float MaxLife = 5f;

        private void Start() {
            Destroy(gameObject, MaxLife);
        }

        private void OnCollisionEnter(Collision other) {
            Destroy(gameObject);
        }
    }
}