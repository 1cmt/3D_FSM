using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    //SO와 연관된 값들임
    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; } 
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public float JumpForce { get; set; }
    //가만히 칼만 휘들르면 멋이없으니 힘을 받으면서 앞으로 나가는듯한 모션이 취해져야 맛이 삼
    

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; } //몇번째 콤보가 진행중인지

    public Transform MainCamTransform { get; set; } //로테이션은 카메라가 필요함

    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerComboAttackState ComboAttackState { get; }


    public PlayerStateMachine(Player player) //생성자
    {
        this.Player = player;

        MainCamTransform = Camera.main.transform;

        IdleState = new PlayerIdleState(this);
        WalkState = new PlayerWalkState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        ComboAttackState = new PlayerComboAttackState(this);

        //SO의 데이터를 가져옴
        MovementSpeed = player.Data.GroundData.BaseSpeed;
        RotationDamping = player.Data.GroundData.BaseRotationDamping;
    }
}