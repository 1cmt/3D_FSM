using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    //공통적으로 공유하는 상태값이 있음, 공통된 기능을 묶어주는 상태임
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;

    public PlayerBaseState(PlayerStateMachine stateMachine) //생성자
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    //들어갔다 나올 때마다 이벤트를 등록해주고 뺴주고 하는거임
    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PPlayerActions.Movement.canceled += OnMovementCanceled;
        input.PPlayerActions.Run.started += OnRunStarted;
        input.PPlayerActions.Jump.started += OnJumpStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.PPlayerActions.Movement.canceled -= OnMovementCanceled;
        input.PPlayerActions.Run.started -= OnRunStarted;
        input.PPlayerActions.Jump.started -= OnJumpStarted;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnRunStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        //점프를 실행해주는 State서 구체적 구현함
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    //애니메이션 전환은 모든 상태에 다 필요함
    protected void StartAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, true);
    }

    //애니메이션 종료도 모든 상태에 다 필요함
    protected void StopAnimation(int animationHash)
    {
        stateMachine.Player.Animator.SetBool(animationHash, false);
    }

    //모든 상태서 필요한 중요한 입력값이 WASD의 MovementInput임, 점프 시에도 해당 방향으로 관성처럼 살짝 밀려나는 효과도 연출돼야함
    //어떤 상태던 상태머신에 있는 MovementInput으로 쓰일 Vector2값을 가져오는 것임
    private void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.Input.PPlayerActions.Movement.ReadValue<Vector2>();
    }

    //base에 Move함수 두고 예외처리하는 방식으로 구현
    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Rotate(movementDirection);

        Move(movementDirection);
    }

    private Vector3 GetMovementDirection()
    {
        //메인카메라와 케릭터가 바라보는 방향을 같게 만들어줄 것이기에
        Vector3 forward = stateMachine.MainCamTransform.forward;
        Vector3 right = stateMachine.MainCamTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize(); //방향값이니 정규화
        right.Normalize();

        //w,s는 y값으로 들어가나 실제론 앞으로가야함, a,d는 그대로 옆이다.
        return forward * stateMachine.MovementInput.y + right * stateMachine.MovementInput.x;
    }

    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();
        //Player Controller가 제공하는 이동관련 함수 Move 사용, 모든 기기서 일정하게 하려고 deltaTime 곱함
        stateMachine.Player.Controller.Move(((direction * movementSpeed) + stateMachine.Player.ForceReceiver.Movement) * Time.deltaTime);
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.Player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction); //LookRotation 함수는 특정 방향을 바라보는 회전값을 계산해준다.
            //서서히 돌 수 있도록 Slerp로 보간한다. 보간 시켜줄 정도는 RotationDamping 값으로
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.RotationDamping * Time.deltaTime);
        }
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return movementSpeed;
    }



}