using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace KWS
{
    public static partial class KWS_CoreUtils
    {
        static bool CanRenderWaterForCurrentCamera_PlatformSpecific(Camera cam)
        {
            var camData = cam.GetComponent<UniversalAdditionalCameraData>();
            if (camData != null && camData.renderType == CameraRenderType.Overlay) return false;
            return true;
        }
        public static Vector2 GetCameraRTHandleViewPortSize(Camera cam)
        {
            var viewPort      = RTHandles.rtHandleProperties.currentRenderTargetSize;
            if(viewPort.x == 0 || viewPort.y == 0) return new Vector2(cam.pixelRect.width, cam.pixelRect.height);

            var rtHandleScale = RTHandles.rtHandleProperties.rtHandleScale;
            return new Vector2Int(Mathf.RoundToInt(rtHandleScale.x * viewPort.x), Mathf.RoundToInt(rtHandleScale.y * viewPort.y));
            
        }
    }
}