using Cobble.SpyHunter.Ai.Actions;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    public class VehicleDespawner : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            var drivingAiAction = other.gameObject.GetComponentInParent<DrivingAiAction>();
            if (drivingAiAction)
                Destroy(drivingAiAction.gameObject);
        }
    }
}