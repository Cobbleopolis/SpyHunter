using System.Collections;
using Cobble.Core.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Player {
    [RequireComponent(typeof(CarController))]
    public class ScoreHandler : MonoBehaviour {
        public int PlayerScore;

        public int IncreaseRate = 15;

        public float CheckRadius = 0.1f;

        public string ScoreArea = "Road";

        public int MinTimerDelay = 5;

        public int MaxTimerDelay = 15;

        [SerializeField] private CarController _carController;

        private int _areaMask;

        private int _currentTimerCount;

        private int _timerCount;

        private void Start() {
            _areaMask = 1 << NavMesh.GetAreaFromName(ScoreArea);
            if (!_carController)
                _carController = GetComponent<CarController>();
        }

        private void Update() {
            if (GameManager.Instance.IsPaused) return;
            _currentTimerCount = (int) Mathf.Lerp(MaxTimerDelay, MinTimerDelay,
                _carController.MovementSettings.CurrentTargetSpeed / _carController.MovementSettings.MaxSpeed);
            NavMeshHit navMeshHit;
            if (_timerCount >= _currentTimerCount &&
                _carController.MovementSettings.CurrentTargetSpeed > float.Epsilon &&
                NavMesh.SamplePosition(transform.position, out navMeshHit, CheckRadius, _areaMask)) {
                PlayerScore += IncreaseRate;
                _timerCount = 0;
            }

            _timerCount++;
        }

        public void DisableScoreHandler(float delayTime) {
            enabled = false;
            StartCoroutine(DisablePause(delayTime));
        }

        private IEnumerator DisablePause(float delayTime) {
            yield return new WaitForSeconds(delayTime);
            enabled = true;
        }
    }
}