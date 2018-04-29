using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Ai.Actions;
using Cobble.SpyHunter.Entity;
using UnityEngine;

namespace Cobble.SpyHunter.Ai.Controllers {
    
    [RequireComponent(typeof(DrivingAiAction))]
    [RequireComponent(typeof(ShotCarAiAction))]
    public class HostileCarAiController : AiController {
        
        private DrivingAiAction _drivingAiAction;

        private ShotCarAiAction _shotCarAiAction;

        private bool _hasBeenHit;

        private void Start() {
            _drivingAiAction = GetComponent<DrivingAiAction>();
            _shotCarAiAction = GetComponent<ShotCarAiAction>();
        }

        protected override AiAction GetCurrentState() {
            return _hasBeenHit ? (AiAction) _shotCarAiAction : _drivingAiAction;
        }

        private void OnCollisionEnter(Collision other) {
            if (!_hasBeenHit && other.gameObject.GetComponent<BulletEntity>())
                _hasBeenHit = true;
        }
    }
}