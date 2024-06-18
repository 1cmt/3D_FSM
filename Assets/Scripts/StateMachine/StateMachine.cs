public interface IState
{
    //각각의 상태들은 이 인터페이스를 상속받게 된다.
    //상태머신은 비디오 플레이어, 상태는 비디오 테잎이라 생각하면 좋음 테잎을 넣고 빼고 테잎을 감고 하는 작업을 인터페이스의 함수로 만들어뒀다 생각하면 편함.
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

public abstract class StateMachine
{
    protected IState currentState;

    public void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    public void HandleInput() 
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}

