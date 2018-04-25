using UnityEngine;

namespace Cobble.Core.Lib.Ui {
    public class AsyncOperationLoadingBar : MonoBehaviour {

        public AsyncOperation Op;

        [SerializeField] private GameObject _progressFilled;

        private void Update() {
            if (!_progressFilled || Op == null) return;
            var scale = _progressFilled.transform.localScale;
            scale.x = Op.progress;
            _progressFilled.transform.localScale = scale;
        }
    }
}