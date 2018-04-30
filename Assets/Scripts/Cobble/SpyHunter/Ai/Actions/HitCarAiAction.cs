using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Ai.Actions {

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class HitCarAiAction : AiAction {

        public bool IsHostile;

        public int PointValue = 100;

        public float ScoreDelayTime = 5f;
        
        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField]
        private Rigidbody _rigidbody;
        
        private void Start() {
            if (!_navMeshAgent)
                _navMeshAgent = GetComponent<NavMeshAgent>();
            
            if (!_rigidbody)
                _rigidbody = GetComponent<Rigidbody>();
        }
        
        protected override void OnActivate() {
            _navMeshAgent.stoppingDistance = 0;
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            var scoreHandler = FindObjectOfType<ScoreHandler>(); 
            if (IsHostile)
                scoreHandler.PlayerScore += PointValue;
            else
                scoreHandler.DisableScoreHandler(ScoreDelayTime);
//            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
//            foreach (var rigidbodyCollider in _rigidbody.GetComponentsInChildren<Collider>())
//                rigidbodyCollider.enabled = false;
        }
        
        public override void Call() {
            
        }

        private void OnBecameInvisible() {
            Destroy(gameObject);
        }
    }
}