using UnityEngine;
using System.Collections;

public class ChangeStateSpeed : StateMachineBehaviour
{
    private PlayerMotor _playerMotor;

    // Use this for initialization
    void Start()
	{
        _playerMotor = PlayerController.Instance?.GetComponent<PlayerMotor>();
	}

    // Update is called once per frame
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.speed = stateInfo.speed * _playerMotor.ZSpeed;
    }
}

