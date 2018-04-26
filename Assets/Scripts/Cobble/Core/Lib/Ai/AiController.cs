using UnityEngine;

namespace Cobble.Core.Lib.AI {
    public abstract class AiController : MonoBehaviour {

        public AiAction CurrentState;
        
        private void Update() {
            SwapState(GetCurrentState());
            CurrentState.Call();
        }

//        protected abstract void SetupDefautState();

        protected abstract AiAction GetCurrentState();

        protected void SwapState(AiAction to) {
            if (CurrentState == to) return;
            if (CurrentState)
                CurrentState.Deactivte();
            CurrentState = to;
            CurrentState.Activate();
        }

    }
}