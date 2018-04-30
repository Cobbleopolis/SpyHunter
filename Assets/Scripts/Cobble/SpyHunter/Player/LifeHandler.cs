using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    [RequireComponent(typeof(ScoreHandler))]
    public class LifeHandler : MonoBehaviour {
        [Tooltip("The amount of time at once the player starts moving that the player has unlimited lives.")]
        public int LeadInCount = 1000;

        public int DecrementDelay = 10;

        public int MaxLives = 5;

        public int PointsForLife = 10000;

        [Tooltip("The number of extra cars that the player has.")] [SerializeField]
        private int _extraLives;

        private int _delayCount;

        private int _lastLifeScore = -1;

        [SerializeField] private ScoreHandler _scoreHandler;

        public int ExtraLives {
            get { return _extraLives; }
        }

        private void Start() {
            if (!_scoreHandler)
                _scoreHandler = GetComponent<ScoreHandler>();
        }

        private void Update() {
            if (GameManager.Instance.IsPaused) return;
            if (LeadInCount > 0) {
                if (_delayCount >= DecrementDelay) {
                    LeadInCount--;
                    _delayCount = 0;
                }                
            } else {
                if (_lastLifeScore == -1) {
                    _lastLifeScore = _scoreHandler.PlayerScore;
                } else if (_scoreHandler.PlayerScore >= _lastLifeScore + PointsForLife){
                    AddLives();
                    _lastLifeScore = _scoreHandler.PlayerScore;
                }
            }

            _delayCount++;
        }

        public void AddLives(int amount = 1) {
            _extraLives = Mathf.Clamp(_extraLives + amount, 0, MaxLives);
        }

        public void RemoveLife(int amount = 1) {
            if (LeadInCount <= 0)
                _extraLives = Mathf.Clamp(_extraLives - amount, 0, MaxLives);
        }

        public bool HasExtraLife() {
            return LeadInCount > 0 || ExtraLives > 0;
        }
    }
}