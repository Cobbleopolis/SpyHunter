using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Cobble.SpyHunter.Ai.Actions {
    [RequireComponent(typeof(NavMeshAgent))]
    public class DrivingAiAction : AiAction {
        
        public string InvalidAreaName = "Walkable";
        
        public float PathThreshold = 5f;

        public float PathDistance = 100f;

        private float _normalSpeed;

        private NavMeshAgent _navMeshAgent;
        
        private int _invalidAreaMask;

        private void Start() {
            if (!_navMeshAgent)
                _navMeshAgent = GetComponent<NavMeshAgent>();
            _normalSpeed = _navMeshAgent.speed;
            
            _invalidAreaMask = 1 << NavMesh.GetAreaFromName(InvalidAreaName);
        }

        protected override void OnActivate() {
            SetRandomDestination();
        }

        public override void Call() {
            if (_navMeshAgent.remainingDistance <= PathThreshold ||
                _navMeshAgent.destination.z < transform.position.z ||
                Mathf.Abs(_navMeshAgent.destination.x - transform.position.x) > float.Epsilon)

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
                if (NavMesh.SamplePosition(samplePos, out hit, PathThreshold, _navMeshAgent.areaMask & ~_invalidAreaMask)) {
                    _navMeshAgent.SetDestination(hit.position);
                }
            }
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Player")) {
                if (Vector3.Dot(transform.forward, (other.transform.position - transform.position).normalized) < 0.5f)
                    _navMeshAgent.speed = other.gameObject.GetComponent<CarController>().MovementSettings
                        .CurrentTargetSpeed;
            }
        }

        private void OnCollisionExit(Collision other) {
            if (other.gameObject.CompareTag("Player"))
                _navMeshAgent.speed = _normalSpeed;
        }
    }
}