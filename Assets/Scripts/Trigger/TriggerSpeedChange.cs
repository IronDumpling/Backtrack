using UnityEngine;

public class TriggerSpeedChange : TriggerBase
{
    [SerializeField] private float speedChange = 1f;
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
            _playerMotor.ZSpeed += speedChange;
    }
}
