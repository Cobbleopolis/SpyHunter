using System.Collections;
using Cobble.SpyHunter.Ai.Actions;
using Cobble.SpyHunter.Entity;
using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Level {
    public class RegionTransitionTrigger : MonoBehaviour {

        public Transform RespawnOrigin;

        public Texture2D[] TerrainTextures;

        public Terrain TerrainObject;

        public float SpawnRenableDelay = 15f;

        private static int _lastTextureIndex;

        private void Start() {
            if (!RespawnOrigin)
                RespawnOrigin = GameObject.FindGameObjectWithTag("Respawn").transform;
        }

        private void OnApplicationQuit() {
            ChangeTerrainTexture(0);
        }

        private void OnTriggerEnter(Collider other) {
            if (!RespawnOrigin || other.GetComponent<VehicleDespawner>()) return;
            TeleportObject(other);
            if (!other.transform.parent || !other.transform.parent.CompareTag("Player")) return;
            HandlePlayerTeleportation(other);
            ChangeRandomTerrainTexture();
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

        private void ChangeRandomTerrainTexture() {
            int textureIndex;
            do {
                textureIndex = Random.Range(0, TerrainTextures.Length);
            } while (textureIndex == _lastTextureIndex);
            ChangeTerrainTexture(textureIndex);
        }

        private void ChangeTerrainTexture(int textureIndex) {
            var splatPrototypes = TerrainObject.terrainData.splatPrototypes;
            if (splatPrototypes[0] == null) return;
            splatPrototypes[0].texture = TerrainTextures[textureIndex];
            TerrainObject.terrainData.splatPrototypes = splatPrototypes;
            _lastTextureIndex = textureIndex;
        }
    }
}