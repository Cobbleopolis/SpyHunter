using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.Core.Lib.Ui {
    public class GuiBase : MonoBehaviour {
        public GuiScreen GuiScreen;

        public bool PausesGame;

        public bool FreeMouse = true;

        private void Awake() { }

        // Use this for initialization
        private void Start() { }

        // Update is called once per frame
        private void Update() { }

        public void OnShow() {
            gameObject.SetActive(true);

            if (PausesGame)
                GameManager.PauseGame();
            else
                GameManager.UnpauseGame();

            if (FreeMouse)
                GuiManager.FreeMouse();
            else
                GuiManager.TrapMouse();
        }

        public void OnHide() {
            gameObject.SetActive(false);
        }
    }
}