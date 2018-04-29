using UnityEngine;

namespace Cobble.Core.Lib {
    public abstract class Manager<T> : MonoBehaviour where T : Manager<T> {

        public static T Instance;

        protected virtual void Awake() {
            if (Instance == null) {
                DontDestroyOnLoad(gameObject);
                Instance = (T) this;
            } else if (Instance != this)
                Destroy(gameObject);
        }
    }
}