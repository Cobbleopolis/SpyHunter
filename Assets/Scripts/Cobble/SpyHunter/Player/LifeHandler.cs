using Cobble.Core.Managers;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class LifeHandler : MonoBehaviour {

        [Tooltip("The amount of time at once the player starts moving that the player has unlimited lives.")]
        public int LeadInCount = 1000;

        public int DecrementDelay = 10;

        [Tooltip("The number of extra cars that the player has.")]
        [SerializeField]
        private int _extraLives;

        private int _delayCount;
        
        public int ExtraLives {
            get { return _extraLives; }
        }

        private void Update() {
            if (GameManager.IsPaused || LeadInCount <= 0) return;
            if (_delayCount >= DecrementDelay) {
                LeadInCount--;
                _delayCount = 0;
            }
            _delayCount++;
//            if (LeadInTimeSeconds > 0)
//                LeadInTimeSeconds -= Time.deltaTime;
        }

        public void AddLives(int amount = 1) {
            _extraLives += amount;
        }

        public void RemoveLife(int amount = 1) {
            if (LeadInCount > 0)
                _extraLives -= amount;
        }

        public bool HasExtraLife() {
            return LeadInCount > 0 || ExtraLives > 0;
        }
        
    }
}