using System.Collections;
using UnityEngine;

namespace Cobble.SpyHunter.Entity {
    public class OffscreenDespawn : MonoBehaviour {

        public float MaxOffScreenTime = 2.5f;

        [SerializeField]
        private GameObject _gameObjectToDestroy;
        
        private Coroutine _despawnCoroutine;

        private void Start() {
            if (!_gameObjectToDestroy)
                _gameObjectToDestroy = gameObject;
        }
        
        private void OnBecameInvisible() {
            if (gameObject.activeInHierarchy)
                _despawnCoroutine = StartCoroutine(DespawnTimer());
        }

        private void OnBecameVisible() {
            if (_despawnCoroutine != null)
                StopCoroutine(_despawnCoroutine);
        }

        private IEnumerator DespawnTimer() {
            yield return new WaitForSeconds(MaxOffScreenTime);
            Destroy(_gameObjectToDestroy);
        }
    }
}