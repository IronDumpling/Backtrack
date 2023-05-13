using System.Collections;
using UnityEngine;
using Common;

// Update LookAtPoint Transformation 
public class PlayerLookAt : MonoSingleton<PlayerLookAt> {

    public float raycastDistance = 1f;

    private Transform _pyCtrlTransform;
    private PlayerMotor _playerMotor;

    [Tooltip("Explose to CameraTrigger")]
    public bool _IsFixOnPath = true;
    [SerializeField] private Vector3 defaultLookAtPointPos = Vector3.up;

    private void FixCameraOnPath() {
        RaycastHit hit;

        if (_playerMotor.RayCastBottom(out hit)) {
            Transform hitTr = hit.collider.transform;
            
            if (!hitTr.name.ToLower().StartsWith("turn")) {
                Vector3 localHitPoint = _pyCtrlTransform.InverseTransformPoint(hitTr.position);
                this.transform.localPosition = new Vector3(localHitPoint.x, this.transform.localPosition.y, this.transform.localPosition.z);
            }
        }
    }

    // Use this for initialization
    void Start() {
        _pyCtrlTransform = PlayerController.Instance.transform;
        _playerMotor = _pyCtrlTransform.GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {
        if (_IsFixOnPath) {
            FixCameraOnPath();
        }
        else {
            this.transform.localPosition = defaultLookAtPointPos;
        }
    }
}
