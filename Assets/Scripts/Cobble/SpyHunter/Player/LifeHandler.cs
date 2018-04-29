using UnityEngine;

namespace Cobble.SpyHunter.Player {
    public class LifeHandler : MonoBehaviour {

        [Tooltip("The amount of time at once the player starts moving that the player has unlimited lives.")]
        public float LeadInTimeSeconds = 70f;

        [Tooltip("The number of extra cars that the player has.")]
        [SerializeField]
        private int _extraLives;
        
        public int ExtraLives {
            get { return _extraLives; }
        }

        private void Update() {
            if (LeadInTimeSeconds > 0)
                LeadInTimeSeconds -= Time.deltaTime;
        }

        public void AddLives(int amount = 1) {
            _extraLives += amount;
        }

        public void RemoveLife(int amount = 1) {
            if (LeadInTimeSeconds > 0)
                _extraLives -= amount;
        }

        public bool HasExtraLife() {
            return LeadInTimeSeconds > 0 || ExtraLives > 0;
        }
        
    }
}