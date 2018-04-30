using System.Collections;
using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Cobble.SpyHunter.Ui {
    [RequireComponent(typeof(Text))]
    public class ScorePanelHud : MonoBehaviour {

        public ScoreHandler ScoreHandler;

        [SerializeField] private float _flashTime = 0.5f;

        private Coroutine _flashingCoroutine;

        [SerializeField]
        private Text _scoreText;

        private void Start() {
            if (!ScoreHandler)
                ScoreHandler = GameObject.FindWithTag("Player").GetComponent<ScoreHandler>();
            if (!_scoreText)
                _scoreText = GetComponent<Text>();
        }

        private void Update() {
            _scoreText.text = ScoreHandler.PlayerScore.ToString();

            if (_flashingCoroutine == null && !ScoreHandler.enabled)
                _flashingCoroutine = StartCoroutine(FlashHandler());
        }

        private IEnumerator FlashHandler() {
            while (!ScoreHandler.enabled) {
                _scoreText.enabled = !_scoreText.enabled;
                yield return new WaitForSeconds(_flashTime);
            }

            _scoreText.enabled = true;
            _flashingCoroutine = null;
        }
    }
}