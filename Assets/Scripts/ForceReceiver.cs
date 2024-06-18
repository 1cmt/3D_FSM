using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    //중력 관련
    //Physics.gravity <- (0, -9.8, 0) 중력은 아래로 작용함
    
    [SerializeField] private CharacterController controller; //CharacterController에 땅에 붙어 있는지 여부를 판단해줄 수 있는 값이 있음
    [SerializeField] private float drag = 0.3f;

    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity; //새로로 영향을 받는 Velocity

    public Vector3 Movement => impact + Vector3.up * verticalVelocity; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded) //땅바닥에 붙어있다면 일반적인 중력가속도 자체만 가지고 있음 9.8의 힘만 유지하고 있음
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        } 
        else //땅에 붙어있지 않다면 지속적으로 계속 중력의 영향을 받을 수 있게 더해줌, 위로 올라갈 수록 절댓값 크기는 커질거임 높을 수록 에너지 크기가 커짐
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce; //기존에 받는 중력가속도 힘에 점프 힘을 더해주는 거임 
    }
}