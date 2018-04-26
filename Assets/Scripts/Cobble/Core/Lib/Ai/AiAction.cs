using UnityEngine;
using UnityEngine.AI;

namespace Cobble.Core.Lib.AI {
    
    
    public abstract class AiAction : MonoBehaviour {

        public bool IsAiActive;

        public void Activate() {
            if (IsAiActive) return;
            IsAiActive = true;
            OnActivate();
        }

        public void Deactivte() {
            if (!IsAiActive) return;
            IsAiActive = false;
            OnDeactivate();
        }
        

        protected virtual void OnActivate() {
            
        }

        protected virtual void OnDeactivate() {
            
        }

        public abstract void Call();

//        protected abstract bool ShouldBeEnabled();
    }
}