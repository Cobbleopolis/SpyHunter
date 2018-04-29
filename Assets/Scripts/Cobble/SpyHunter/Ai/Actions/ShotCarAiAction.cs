using Cobble.Core.Lib.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.SpyHunter.Ai.Actions {

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class ShotCarAiAction : AiAction {
        
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
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            foreach (var rigidbodyCollider in _rigidbody.GetComponentsInChildren<Collider>())
                rigidbodyCollider.enabled = false;
        }
        
        public override void Call() {
            
        }

        private void OnBecameInvisible() {
            Destroy(gameObject);
        }
    }
}