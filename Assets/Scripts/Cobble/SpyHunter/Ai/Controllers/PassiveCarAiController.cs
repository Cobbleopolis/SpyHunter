using Cobble.Core.Lib.AI;
using Cobble.SpyHunter.Ai.Actions;
using UnityEngine;

namespace Cobble.SpyHunter.Ai.Controllers {
    
    [RequireComponent(typeof(DrivingAiAction))]
    public class PassiveCarAiController : AiController {

        private DrivingAiAction _drivingAiAction;

        private void Start() {
            _drivingAiAction = GetComponent<DrivingAiAction>();
        }

        protected override AiAction GetCurrentState() {
            return _drivingAiAction;
        }
    }
}