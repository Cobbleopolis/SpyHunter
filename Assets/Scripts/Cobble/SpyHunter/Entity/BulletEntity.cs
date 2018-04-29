using Cobble.SpyHunter.Ai.Controllers;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    [RequireComponent(typeof(Rigidbody))]
    public class BulletEntity : MonoBehaviour {

        public float MaxLife = 5f;

        private void Start() {
            Destroy(gameObject, MaxLife);
        }

        private void OnCollisionEnter(Collision other) {
            var carAiController = other.gameObject.GetComponent<CarAiController>();
            if (carAiController)
                carAiController.HasBeenHit = true;
            other.rigidbody.velocity = Vector3.zero;
            Destroy(gameObject);
        }

        private void OnBecameInvisible() {
            Destroy(gameObject);
        }
    }
}