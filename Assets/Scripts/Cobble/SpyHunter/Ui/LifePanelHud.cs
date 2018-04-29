using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.SpyHunter.Ui {
    public class LifePanelHud : MonoBehaviour {
        public LifeHandler PlayerLifeHandler;

        public Text TimerText;

        private void Start() {
            if (!PlayerLifeHandler)
                PlayerLifeHandler = GameObject.FindWithTag("Player").GetComponent<LifeHandler>();
        }

        private void Update() {
            if (PlayerLifeHandler.LeadInCount > 0) {
                TimerText.text = PlayerLifeHandler.LeadInCount.ToString();
            } else {
                if (TimerText.gameObject.activeInHierarchy)
                    TimerText.gameObject.SetActive(false);
            }
        }
    }
}