using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //input system 관련 스크립트
    public PlayerInputs PPlayerInputs { get; private set; }
    public PlayerInputs.PlayerActions PPlayerActions { get; private set; } //PlayerInputs 안의 PlayerActions 데이터를 담아둠

    private void Awake()
    {
        PPlayerInputs = new PlayerInputs();
        PPlayerActions = PPlayerInputs.Player; //Player는 Actions Map의 Player를 의미
    }

    private void OnEnable()
    {
        PPlayerInputs.Enable(); //켜질 떄 인풋 Enable    
    }

    private void OnDisable()
    {
        PPlayerInputs.Disable();
    }
}
