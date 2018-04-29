using System.Collections;
using System.Linq;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cobble.Core.Lib.Ui {
    
    [RequireComponent(typeof(CanvasRenderer))]
    public class GuiBase : MonoBehaviour {
        
        public GuiScreen GuiScreen;

        public bool PausesGame;

        public bool FreeMouse = true;

        public bool MouseHiddenUntilMove;

        public Selectable DefaultSelectable;


        public void OnShow() {
            gameObject.SetActive(true);

            if (PausesGame)
                GameManager.Instance.PauseGame();
            else
                GameManager.Instance.UnpauseGame();

            if (FreeMouse)
                if (MouseHiddenUntilMove && GuiManager.CanHideMouseUntilMove()) {
                        StartCoroutine(MouseMoveHandler());
                    if (DefaultSelectable)
                        DefaultSelectable.Select();
                } else
                    GuiManager.Instance.FreeMouse();
            else
                GuiManager.Instance.TrapMouse();
        }

        private static IEnumerator MouseMoveHandler() {
            var initalMousePosition = Input.mousePosition;
            yield return new WaitUntil(() => initalMousePosition != Input.mousePosition);
            GuiManager.Instance.FreeMouse();
            if (EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(null);
        }

        public void OnHide() {
            gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }

        
    }
}