using UnityEngine;

public class TriggerSpeedChange : TriggerBase
{
    [SerializeField] private float _ZSpeedChange = 0.5f;
    [SerializeField] private float _XSpeedChange = 0f;

    private PlayerMotor _playerMotor;

    private void Start()
    {
        _playerMotor = PlayerController.Instance?.GetComponent<PlayerMotor>();
        if (_playerMotor == null)
            Debug.LogWarning("Player Motor Missing");
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        if(_playerMotor != null)
        {
            _playerMotor.ZSpeed += _ZSpeedChange;
            _playerMotor.XSpeed += _XSpeedChange;
        }
            
    }
}
