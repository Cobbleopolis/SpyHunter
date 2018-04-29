using Cobble.SpyHunter.Ai.Controllers;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    public class OilSlickEntity : MonoBehaviour {
        
        public float RotationMagnituede = 50f;
        
        public float ForceMagnituede = 50f;

        public ForceMode ApplyForceMode = ForceMode.VelocityChange;

        public float MaxLife = 10f;

        private void Start() {
            Destroy(gameObject, MaxLife);
        }

        private void OnTriggerEnter(Collider other) {
            var carAiController = other.GetComponentInParent<CarAiController>(); 
            if (!carAiController || carAiController.HasBeenHit) return;
            carAiController.HasBeenHit = true;
            var torqueVec = Vector3.up * RotationMagnituede;
            var forceDirection = other.transform.right * ForceMagnituede;
            if (other.transform.position.x < transform.position.x) {
                torqueVec = -torqueVec;
                forceDirection = -forceDirection;
            }
            other.attachedRigidbody.AddRelativeTorque(torqueVec, ApplyForceMode);
            other.attachedRigidbody.velocity = forceDirection;
        }
    }
}