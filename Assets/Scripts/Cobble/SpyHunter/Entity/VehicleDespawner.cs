using Cobble.SpyHunter.AI;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    public class VehicleDespawner : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            var carAi = other.gameObject.GetComponentInParent<CarAi>(); 
            if (carAi)
                Destroy(carAi.gameObject);
        }
    }
}