using System.Collections;
using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    public class VehicleSpawner : MonoBehaviour {

        public Collider SpawnArea;

        public float SpawnTimer = 5f;

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
                _bounds = SpawnArea.bounds;
                if (GameManager.IsPaused) continue;
                var spawnLoaction = new Vector3(
                    Random.Range(_bounds.min.x, _bounds.max.x),
                    Random.Range(_bounds.min.y, _bounds.max.y),
                    Random.Range(_bounds.min.z, _bounds.max.z)
                );
                SpawnVehicle(spawnLoaction);
            }
        }

        private void SpawnVehicle(Vector3 spawnLocation) {
            Instantiate(VehiclePrefabs[Random.Range(0, VehiclePrefabs.Length)], spawnLocation, Quaternion.identity);
        }
    }
}