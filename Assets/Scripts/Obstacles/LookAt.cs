using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Obstacles {
    public class LookAt : MonoBehaviour {

        public Transform LookAtTarget;

        private void Awake() {
            if (LookAtTarget == null) {
                DebugLogger.Warning(this.name, "LookAtTarget not set!");
            }
        }

        private void Update() {
            if (LookAtTarget != null)
                this.transform.LookAt(LookAtTarget);
        }
    }
}