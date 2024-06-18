using UnityEngine;

public class Player : MonoBehaviour
{
    //플레이어에게 필요한 여러 컴포넌트들을 관리하는 클래스
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    //public이여도 private set이라 인스팩터창서 안보여 SerializeField 달아줌. field:는 필드에 보여주기 위함임을 보여줌
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Animator Animator { get; private set; }
    public PlayerController Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>(); //애니메이터는 모델쪽에 붙였으니 자식중에 찾아야함
        Input = GetComponent<PlayerController>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        //Start하면 커서는 가려주도록
        Cursor.lockState = CursorLockMode.Locked;
        //첨엔 Idle 상태로 시작할테니 Idle 상태로 변화시킴
        stateMachine.ChangeState(stateMachine.IdleState);
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
