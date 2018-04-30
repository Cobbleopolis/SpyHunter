using Cobble.Core.Lib.Ui;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.SpyHunter.Ui {
    public class GameOverUi : MonoBehaviour {
        
        public AsyncOperationLoadingBar LoadingBar;

        private void Start() {
            GuiManager.Instance.Open(GuiScreen.GameOver);
        }

        public void LoadSceneAsync(string sceneName) {
            LoadingBar.gameObject.SetActive(true);
            LoadingBar.Op = SceneManager.LoadSceneAsync(sceneName);
        }
    }
}