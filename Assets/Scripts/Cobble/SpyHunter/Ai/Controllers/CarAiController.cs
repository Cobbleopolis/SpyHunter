using System;
using System.Collections;
using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Ai.Actions;
using Cobble.SpyHunter.Entity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Ai.Controllers {
    
    [RequireComponent(typeof(DrivingAiAction))]
    [RequireComponent(typeof(HitCarAiAction))]
    public class CarAiController : AiController {

        public string InvalidAreaName = "Walkable";

        public float RoadCheckRadius = 0.25f;
        
        public bool HasBeenHit;
        
        [SerializeField]
        private DrivingAiAction _drivingAiAction;

        [SerializeField]
        private HitCarAiAction _hitCarAiAction;

//        private bool _hasBeenHit;
        private int _invalidAreaMask;

        private void Start() {
            if (!_drivingAiAction)
                _drivingAiAction = GetComponent<DrivingAiAction>();
            
            if (!_hitCarAiAction)
                _hitCarAiAction = GetComponent<HitCarAiAction>();

            _invalidAreaMask = NavMesh.GetAreaFromName(InvalidAreaName);
        }

        protected override AiAction GetCurrentState() {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, RoadCheckRadius, _invalidAreaMask)) {
                HasBeenHit = true;
                _hitCarAiAction.DelayScore = false;
            }

            return HasBeenHit ? (AiAction) _hitCarAiAction : _drivingAiAction;
        }

        private void OnCollisionEnter(Collision other) {
            if (!HasBeenHit && other.gameObject.GetComponent<BulletEntity>())
                HasBeenHit = true;
        }
    }
}