using UnityEngine;

public class TriggerSpeedChange : TriggerBase
{
    [SerializeField] private float _ZSpeedChange = 0.5f;
    [SerializeField] private float _XSpeedChange = 0f;
    [SerializeField] private bool changeBackOnExit = false;
    [SerializeField] private float _timeScaleInside = 1f;

    private float _timeScaleOutside;
    private PlayerMotor _playerMotor;

    private void Start()
    {
        _playerMotor = PlayerController.Instance?.GetComponent<PlayerMotor>();
        _timeScaleOutside = Time.timeScale;
        if (_playerMotor == null)
            Debug.LogWarning("Player Motor Missing");
    }

    protected override void enterEvent()
    {
        base.enterEvent();
        _timeScaleOutside = Time.timeScale;
        Time.timeScale = _timeScaleInside;
        if(_playerMotor != null)
        {
            _playerMotor.ZSpeed += _ZSpeedChange;
            _playerMotor.XSpeed += _XSpeedChange;
        }
    }

    protected override void ExitEvent()
    {
        base.ExitEvent();
        Time.timeScale = _timeScaleOutside;
        if (changeBackOnExit && _playerMotor != null)
        {
            _playerMotor.ZSpeed -= _ZSpeedChange;
            _playerMotor.XSpeed -= _XSpeedChange;
        }
    }
}
