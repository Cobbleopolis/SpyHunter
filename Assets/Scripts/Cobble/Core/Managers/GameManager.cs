using Cobble.Core.Lib;
using Cobble.Core.Lib.Registry;
using UnityEngine;

namespace Cobble.Core.Managers {
	[DisallowMultipleComponent]
	public class GameManager : Manager<GameManager> {

		public bool IsPaused;

		public bool IsQuitting;

		protected override void Awake() {
			base.Awake();
			GuiManager.Instance.TrapMouse();
			ItemRegistry.RegisterItems();
		}

		private void Start() {

		}

		private void Update() { }

		private void OnApplicationQuit() {
			IsQuitting = true;
		}

		public void PauseGame() {
			IsPaused = true;
			Time.timeScale = 0;
		}

		public void UnpauseGame() {
			IsPaused = false;
			Time.timeScale = 1;
		}

		public void ExitGame() {
			Application.Quit();
		}
	}
}