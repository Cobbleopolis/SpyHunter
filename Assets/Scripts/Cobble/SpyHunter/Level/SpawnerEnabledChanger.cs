using Cobble.SpyHunter.Entity;
using UnityEngine;

namespace Cobble.SpyHunter.Level {
    public class SpawnerEnabledChanger : MonoBehaviour {

        public bool SetState = true;

        private void OnTriggerEnter(Collider other) {
            var vehicleSpawner = other.GetComponentInChildren<VehicleSpawner>(true);
            if (vehicleSpawner)
                vehicleSpawner.enabled = SetState;
        }
    }
}