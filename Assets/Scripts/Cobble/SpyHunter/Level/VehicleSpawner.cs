using System.Collections;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Level {
    public class VehicleSpawner : MonoBehaviour {
        public Collider SpawnArea;

        public float SpawnTimer = 5f;

        public float SpeedVariant = 25f;

        public float MaxSpawnDistance = 1950f;

        public GameObject[] VehiclePrefabs;

        private Bounds _bounds;

        private void Start() {
            if (!SpawnArea)
                SpawnArea = GetComponent<Collider>();
            if (SpawnArea)
                _bounds = SpawnArea.bounds;
        }

        private void OnEnable() {
            StartCoroutine(SpawnHandler());
        }

        private IEnumerator SpawnHandler() {
            while (enabled) {
                yield return new WaitForSeconds(SpawnTimer);
                if (GameManager.Instance.IsPaused || transform.position.x >= MaxSpawnDistance) continue;
                _bounds = SpawnArea.bounds;
                var spawnLoaction = new Vector3(
                    Random.Range(_bounds.min.x, _bounds.max.x),
                    Random.Range(_bounds.min.y, _bounds.max.y),
                    Random.Range(_bounds.min.z, _bounds.max.z)
                );
                SpawnVehicle(spawnLoaction);
            }
        }

        private void SpawnVehicle(Vector3 spawnLocation) {
            var vehicleGameObject = Instantiate(VehiclePrefabs[Random.Range(0, VehiclePrefabs.Length)], spawnLocation,
                Quaternion.identity);
            var vehicleNavMeshAgent = vehicleGameObject.GetComponent<NavMeshAgent>();
            vehicleNavMeshAgent.speed = Random.Range(vehicleNavMeshAgent.speed,
                vehicleNavMeshAgent.speed + SpeedVariant);
        }
    }
}