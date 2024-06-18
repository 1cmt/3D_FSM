using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    //스위치문 필요없이 각자 상태서 필요한 것을 개발

    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = 0f; //가만이 있어야하니 0으로
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.MovementInput != Vector2.zero) //zero가 아니면 움직여야지
        {
            stateMachine.ChangeState(stateMachine.WalkState);
            return;
        }
    }
}