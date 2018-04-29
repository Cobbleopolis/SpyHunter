using System.Collections.Generic;
using Cobble.Core.Lib;
using Cobble.Core.Lib.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.Core.Managers {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GameManager))]
    public class GuiManager : Manager<GuiManager> {

        public GuiScreen CurrentGuiScreen {
            get { return _guiHistory.Count > 0 ? _guiHistory.Peek() : GuiScreen.None; }
        }
        private readonly Dictionary<GuiScreen, GuiBase> _guiScreens = new Dictionary<GuiScreen, GuiBase>();

        private readonly Stack<GuiScreen> _guiHistory = new Stack<GuiScreen>();

        [SerializeField] private GameObject _rootCanvasGameObject;
        private Canvas _rootCanvas;

        protected override void Awake() {
            base.Awake();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void Start() {
            foreach (var guiBase in _rootCanvasGameObject.GetComponentsInChildren<GuiBase>(true)) {
                if (!_guiScreens.ContainsKey(guiBase.GuiScreen))
                    _guiScreens.Add(guiBase.GuiScreen, guiBase);
            }
        }

        private void Update() { }

        private void OnActiveSceneChanged(Scene current, Scene next) {
            CloseAll();
        }

        public GuiBase GetGuiBase(GuiScreen guiScreen) {
            return _guiScreens[guiScreen];
        }

        public void Open(GuiScreen guiScreen) {
            if (CurrentGuiScreen == guiScreen) return;
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Peek()).OnHide();
            if (guiScreen == GuiScreen.None) {
                _guiHistory.Clear();
                GameManager.Instance.UnpauseGame();
                TrapMouse();
            } else if (_guiScreens.ContainsKey(guiScreen)) {
                _guiHistory.Push(guiScreen);
                var guiBase = GetGuiBase(guiScreen);
                guiBase.OnShow();
            }
        }

        public void OpenAndClearHistory(GuiScreen guiScreen) {
            _guiHistory.Clear();
            Open(guiScreen);
        }

        public void GoBack() {
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Pop()).OnHide();
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Peek()).OnShow();
            if (CurrentGuiScreen != GuiScreen.None) return;
            GameManager.Instance.UnpauseGame();
            TrapMouse();
        }

        public void CloseAll() {
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Pop()).OnHide();
            _guiHistory.Clear();
            Open(GuiScreen.None);
        }

        public void FreeMouse() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void TrapMouse() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public bool IsMouseFree() {
            return Cursor.lockState != CursorLockMode.Locked;
        }
    }
}