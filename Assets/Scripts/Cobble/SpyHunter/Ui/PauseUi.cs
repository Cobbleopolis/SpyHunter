using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.SpyHunter.Ui {
    public class PauseUi : MonoBehaviour {

        public void LoadScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
    }
}