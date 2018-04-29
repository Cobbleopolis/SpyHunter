using System.Collections;
using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class BulletAbility : MonoBehaviour {

        public GameObject BulletPrefab;
        
        public Vector3 BulletVelocity = Vector3.forward * 30f;

        public Transform[] BulletSpawnLocations;

        public float SpawnDelay = 0.1f;

        public Collider PlayerCollider;

        public Rigidbody PlayerRigidbody;

        private void Update() {
            if (!GameManager.Instance.IsPaused && Input.GetButtonDown("Fire1"))
                StartCoroutine(FireBullet());
        }

        private IEnumerator FireBullet() {
            while (!GameManager.Instance.IsPaused && Input.GetButton("Fire1")) {
                foreach (var spawnTransform in BulletSpawnLocations)
                    SpawnBullet(spawnTransform);
                yield return new WaitForSeconds(SpawnDelay);
            }
        }

        private void SpawnBullet(Transform spawnTransform) {
            var bulletGameObject = Instantiate(BulletPrefab, spawnTransform.position,
                spawnTransform.rotation);

            var bulletCollider = bulletGameObject.GetComponent<Collider>();
            if(bulletCollider)
                Physics.IgnoreCollision(PlayerCollider, bulletCollider);
                
            var bulletRigidbody = bulletGameObject.GetComponent<Rigidbody>();
            if (!bulletRigidbody) return;
            var relativePlayerVel = PlayerRigidbody.velocity;
            relativePlayerVel.y = 0;
            bulletRigidbody.AddRelativeForce(BulletVelocity + relativePlayerVel, ForceMode.VelocityChange);
        }
    }
}