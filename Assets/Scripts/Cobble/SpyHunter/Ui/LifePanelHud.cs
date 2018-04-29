using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.SpyHunter.Ui {
    public class LifePanelHud : MonoBehaviour {
        public LifeHandler PlayerLifeHandler;

        public Text TimerText;

        public GameObject LifeContainer;

        public GameObject LifeIconPrefab;

        private void Start() {
            if (!PlayerLifeHandler)
                PlayerLifeHandler = GameObject.FindWithTag("Player").GetComponent<LifeHandler>();
        }

        private void Update() {
            if (PlayerLifeHandler.LeadInCount > 0) {
                if (LifeContainer.activeInHierarchy)
                    LifeContainer.SetActive(false);
                TimerText.text = PlayerLifeHandler.LeadInCount.ToString();
            } else {
                if (TimerText.gameObject.activeInHierarchy) {
                    TimerText.gameObject.SetActive(false);
                    LifeContainer.SetActive(true);
                }

                var childCount = LifeContainer.transform.childCount;
                while (childCount < PlayerLifeHandler.ExtraLives) {
                    Instantiate(LifeIconPrefab, LifeContainer.transform);
                    childCount++;
                }

                while (childCount > PlayerLifeHandler.ExtraLives)
                    Destroy(LifeContainer.transform.GetChild(--childCount).gameObject);
            }
        }
    }
}