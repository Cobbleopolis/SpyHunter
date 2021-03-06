﻿using System;
using Cobble.Core.Lib.Ui;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.SpyHunter.Ui {
    public class MainMenuUi : MonoBehaviour {
        
        public AsyncOperationLoadingBar LoadingBar;

        private void Start() {
            GuiManager.Instance.Open(GuiScreen.MainMenu);
        }

        public void LoadSceneAsync(string sceneName) {
            LoadingBar.gameObject.SetActive(true);
            LoadingBar.Op = SceneManager.LoadSceneAsync(sceneName);
        }

        public void ExitGame() {
            GameManager.Instance.ExitGame();
        }
        
    }
}