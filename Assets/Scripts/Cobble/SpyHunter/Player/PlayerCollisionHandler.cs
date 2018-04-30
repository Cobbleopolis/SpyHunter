using System.Collections;
using System.Linq;
using UnityEngine;

namespace Cobble.SpyHunter.Player {
    
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CarController))]
    [RequireComponent(typeof(LifeHandler))]
    public class PlayerCollisionHandler : MonoBehaviour {

        public string VehicleLayerMaskName = "Vehicles";
        
        public string PlayerLayerMaskName = "Player";

        public float ImpulseThreshold = 5f;

        [Range(0, 1)]
        public float DotProductThreshold = 0.75f;

        public float DeathDuration = 5f;

        public float FlashRate = 0.25f;

        [SerializeField] private Rigidbody _rigidbody;
        
        [SerializeField] private CarController _carController;
        
        [SerializeField] private LifeHandler _lifeHandler;

        [SerializeField] private MeshRenderer _meshRenderer;
        
        [SerializeField] private TrailRenderer _trailRenderer;

        private int _vehicleLayerMask;
        
        private int _playerLayerMask;

        private void Start() {
            if (!_rigidbody)
                _rigidbody = GetComponent<Rigidbody>();
            
            if (!_carController)
                _carController = GetComponent<CarController>();
            
            if (!_lifeHandler)
                _lifeHandler = GetComponent<LifeHandler>();
            
            if (!_meshRenderer)
                _meshRenderer = GetComponentInChildren<MeshRenderer>();

            if (!_trailRenderer)
                _trailRenderer = GetComponentInChildren<TrailRenderer>();
            
            _vehicleLayerMask = LayerMask.NameToLayer(VehicleLayerMaskName);
            _playerLayerMask = LayerMask.NameToLayer(PlayerLayerMaskName);
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.layer != _vehicleLayerMask) return;

            var isFrontOrBehind = other.contacts.Any(contactPoint =>
                Mathf.Abs(Vector3.Dot(contactPoint.normal, transform.forward)) >= DotProductThreshold);

            if (!isFrontOrBehind || !_carController.IsAccelerating &&
                                     !(other.impulse.sqrMagnitude >= ImpulseThreshold * ImpulseThreshold)) return;
            if (_lifeHandler.HasExtraLife())
                OnCarDestroied();
            else
                Debug.Log("Game Over");
        }

        private void OnCarDestroied() {
            _lifeHandler.RemoveLives();
            _carController.enabled = false;
            _carController.MovementSettings.CurrentTargetSpeed = 0;
            _trailRenderer.enabled = false;
            Physics.IgnoreLayerCollision(_playerLayerMask, _vehicleLayerMask, true);
            StartCoroutine(UndoCarDestroied());
            StartCoroutine(FlashPlayer());
        }

        private IEnumerator UndoCarDestroied() {
            yield return new WaitForSeconds(DeathDuration);
            _carController.enabled = true;
            _trailRenderer.enabled = true;
            Physics.IgnoreLayerCollision(_playerLayerMask, _vehicleLayerMask, false);
        }

        private IEnumerator FlashPlayer() {
            while (!_carController.enabled) {
                _meshRenderer.enabled = !_meshRenderer.enabled;
                yield return new WaitForSeconds(FlashRate);
            }

            _meshRenderer.enabled = true;
        }
    }
}