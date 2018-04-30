using System;
using System.Collections.Generic;
using System.Linq;
using Cobble.Core.Lib;
using Cobble.Core.Lib.Ui;
using Cobble.Core.UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cobble.Core.Managers {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GameManager))]
    public class GuiManager : Manager<GuiManager> {
        [TagSelector] public string GuiCanvasTag = "Gui Canvas";

        public GuiScreen CurrentGuiScreen {
            get { return _guiHistory.Count > 0 ? _guiHistory.Peek() : GuiScreen.None; }
        }

        private readonly Dictionary<GuiScreen, GuiBase> _guiScreens = new Dictionary<GuiScreen, GuiBase>();

        private readonly Stack<GuiScreen> _guiHistory = new Stack<GuiScreen>();

        [SerializeField] private GameObject _guiCanvas;

        protected override void Awake() {
            base.Awake();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene current, Scene next) {
            _guiHistory.Clear();
            _guiScreens.Clear();
            _guiCanvas = GameObject.FindWithTag(GuiCanvasTag);
            FindAllGuiBases();
        }

        private void FindAllGuiBases() {
            if (!_guiCanvas) return;
            foreach (var guiBase in _guiCanvas.GetComponentsInChildren<GuiBase>(true)) {
                if (!_guiScreens.ContainsKey(guiBase.GuiScreen))
                    _guiScreens.Add(guiBase.GuiScreen, guiBase);
            }
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public GuiBase GetGuiBase(GuiScreen guiScreen) {
            return _guiScreens[guiScreen];
        }

        public void Open(GuiScreen guiScreen) {
            if (!_guiCanvas) {
                Debug.LogWarning("No GUI Canvas was found in the current scene. Unable to preform GUI operations.");
                return;
            }

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
            if (!_guiCanvas) {
                Debug.LogWarning("No GUI Canvas was found in the current scene. Unable to preform GUI operations.");
                return;
            }

            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Pop()).OnHide();
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Peek()).OnShow();
            if (CurrentGuiScreen != GuiScreen.None) return;
            GameManager.Instance.UnpauseGame();
            TrapMouse();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void CloseAll() {
            if (_guiHistory.Count > 0)
                GetGuiBase(_guiHistory.Pop()).OnHide();
            _guiHistory.Clear();
            Open(GuiScreen.None);
        }

        public static void FreeMouse() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public static void TrapMouse() {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public static bool IsMouseFree() {
            return Cursor.lockState != CursorLockMode.Locked;
        }

        public static bool CanHideMouseUntilMove() {
            return Input.mousePresent && JoysticsPresent();
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool JoysticsPresent() {
            var names = Input.GetJoystickNames();
            return names.Any() && names.Any(name => !string.IsNullOrEmpty(name));
        }
    }
}