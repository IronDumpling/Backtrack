using UnityEngine;
using Cinemachine;

namespace CustomizeCamera {
    public class TriggerCameraShake : TriggerBase {

        public ScreenShakeProfile m_Profile;

        protected override void enterEvent() {
            base.enterEvent();
            CameraShakeManager.Instance.ScreenShakeFromProfile(m_Profile);
        }
    }
}