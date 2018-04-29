using System.Collections;
using Cobble.SpyHunter.Ai.Actions;
using Cobble.SpyHunter.Entity;
using Cobble.SpyHunter.Managers;
using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Level {
    public class RegionTransitionTrigger : MonoBehaviour {

        public Transform RespawnOrigin;

        public float SpawnRenableDelay = 15f;
        
        private LevelManager _levelManager;

        private static int _lastTextureIndex;

        private void Start() {
            if (!RespawnOrigin)
                RespawnOrigin = GameObject.FindGameObjectWithTag("Respawn").transform;

            if (!_levelManager)
                _levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnTriggerEnter(Collider other) {
            if (!RespawnOrigin || other.GetComponent<VehicleDespawner>()) return;
            TeleportObject(other);
            if (!other.transform.parent || !other.transform.parent.CompareTag("Player")) return;
            HandlePlayerTeleportation(other);
            _levelManager.OnRegionTransition();
        }

        private void TeleportObject(Component other) {
            var teleportLocation = RespawnOrigin.position - (gameObject.transform.position - other.transform.position);
            
            var navMeshAgent = other.GetComponentInParent<NavMeshAgent>();
            if (navMeshAgent)
                navMeshAgent.Warp(teleportLocation);
            else
                other.transform.position = teleportLocation;
            
            var drivingAiAction = other.GetComponentInParent<DrivingAiAction>();
            if (drivingAiAction)
                drivingAiAction.SetRandomDestination();
        }

        private void HandlePlayerTeleportation(Component other) {
            var trail = other.transform.parent.GetComponentInChildren<TrailRenderer>();
            if (trail)
                trail.Clear();
            var vehicleSpawner = other.transform.parent.GetComponentInChildren<VehicleSpawner>();
            if (!vehicleSpawner) return;
            vehicleSpawner.enabled = false;
            StartCoroutine(ReenableVehicleSpawner(vehicleSpawner));
        }

        private IEnumerator ReenableVehicleSpawner(Behaviour vehicleSpawner) {
            yield return new WaitForSeconds(SpawnRenableDelay);
            vehicleSpawner.enabled = true;
        }
    }
}