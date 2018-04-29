using Cobble.SpyHunter.Ai.Controllers;
using Cobble.SpyHunter.Entity;
using UnityEngine;

namespace Cobble.SpyHunter.Level {
    public class VehicleDespawner : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            var needsToDespawn = other.gameObject.GetComponentInParent<CarAiController>() ||
                                 other.gameObject.GetComponentInParent<OilSlickEntity>();
            if (needsToDespawn)
                Destroy(other.attachedRigidbody.gameObject);
        }
    }
}