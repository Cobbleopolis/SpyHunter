using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Cobble.SpyHunter.AI {
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class CarAi : MonoBehaviour {

        public float PathThreshold = 5f;

        public float PathDistance = 100f;

        public float MaxDistance = 1000f;

        private NavMeshAgent _navMeshAgent;

        private void Start() {
            if (!_navMeshAgent)
                _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.SetDestination(GetRandomPointOnRoad());
        }

        private void Update() {
            if (_navMeshAgent.remainingDistance <= PathThreshold && transform.position.z < MaxDistance - PathDistance)
                _navMeshAgent.SetDestination(GetRandomPointOnRoad());
            Debug.DrawRay(_navMeshAgent.destination, Vector3.up, Color.blue, 0, false);
        }

        private Vector3 GetRandomPointOnRoad() {
            NavMeshHit hit;
            var samplePos = new Vector3(Random.Range(30, 70), transform.position.y, Math.Min(transform.position.z + PathDistance, MaxDistance));
            return NavMesh.SamplePosition(samplePos, out hit, PathThreshold, _navMeshAgent.areaMask) ? hit.position : Vector3.zero;
        }
        
    }
}