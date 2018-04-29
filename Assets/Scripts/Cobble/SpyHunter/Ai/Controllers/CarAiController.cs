using System;
using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Ai.Actions;
using Cobble.SpyHunter.Entity;
using UnityEngine;

namespace Cobble.SpyHunter.Ai.Controllers {
    
    [RequireComponent(typeof(DrivingAiAction))]
    [RequireComponent(typeof(HitCarAiAction))]
    public class CarAiController : AiController {
        private DrivingAiAction _drivingAiAction;

        private HitCarAiAction _hitCarAiAction;

//        private bool _hasBeenHit;

        public bool HasBeenHit;

        private void Start() {
            _drivingAiAction = GetComponent<DrivingAiAction>();
            _hitCarAiAction = GetComponent<HitCarAiAction>();
        }

        protected override AiAction GetCurrentState() {
            return HasBeenHit ? (AiAction) _hitCarAiAction : _drivingAiAction;
        }

        private void OnCollisionEnter(Collision other) {
            if (!HasBeenHit && other.gameObject.GetComponent<BulletEntity>())
                HasBeenHit = true;
        }
    }
}