using Cobble.Core.Lib.AI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Cobble.SpyHunter.Ai.Actions {
    [RequireComponent(typeof(NavMeshAgent))]
    public class DrivingAiAction : AiAction {
        public float PathThreshold = 5f;

        public float PathDistance = 100f;

        private NavMeshAgent _navMeshAgent;

        private void Start() {
            if (!_navMeshAgent)
                _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void OnActivate() {
            SetRandomDestination();
        }

        public override void Call() {
            if (_navMeshAgent.remainingDistance <= PathThreshold)
                SetRandomDestination();
        }

        public void SetRandomDestination() {
            NavMeshHit hit;

            var samplePos = new Vector3(transform.position.x, transform.position.y,
                transform.position.z + PathDistance);
            
            if (NavMesh.SamplePosition(samplePos, out hit, PathThreshold, _navMeshAgent.areaMask))
                _navMeshAgent.SetDestination(hit.position);
            else {
                samplePos = new Vector3(Random.Range(transform.position.x - 15f, transform.position.x + 15f),
                    transform.position.y, transform.position.z + PathDistance);
                if (NavMesh.SamplePosition(samplePos, out hit, PathThreshold, _navMeshAgent.areaMask)) {
                    _navMeshAgent.SetDestination(hit.position);
                }
            }
        }
    }
}