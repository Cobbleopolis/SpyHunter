using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.SpyHunter.Managers {
    public class LevelManager : MonoBehaviour {

        private void Start() {
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnDestroy() {
//            SceneManager.sceneLoaded -= OnSceneLoad;
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode) {
            GameManager.UnpauseGame();
        }
    }
}