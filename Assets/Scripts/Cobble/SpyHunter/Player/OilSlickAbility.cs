using System.Collections;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class OilSlickAbility : MonoBehaviour {

        public GameObject OilSlickPrefab;
        
        public Transform OilSpawnLocation;
        
        public float SpawnDelay = 0.05f;

        public float MaxSpawnTime = 0.75f;

        public Collider PlayerCollider;

        private float _currentSpawnTime;

        private void Update() {
            if (Input.GetButtonDown("Fire2"))
                StartCoroutine(SpawnOilSlick());
        }

        private IEnumerator SpawnOilSlick() {
            while (_currentSpawnTime <= MaxSpawnTime && Input.GetButton("Fire2")) {
                SpawnPrefab();
                yield return new WaitForSeconds(SpawnDelay);
                _currentSpawnTime += SpawnDelay;
            }

            _currentSpawnTime = 0;
        }

        private void SpawnPrefab() {
            var oilSlickGameObject = Instantiate(OilSlickPrefab, OilSpawnLocation.position, OilSpawnLocation.rotation);
            var oilSlickCollider = oilSlickGameObject.GetComponentInChildren<Collider>();
            Physics.IgnoreCollision(oilSlickCollider, PlayerCollider);
        }
    }
}