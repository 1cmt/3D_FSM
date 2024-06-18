using System;
using UnityEngine;

//SO에 들어갈 데이터들을 클래스로 묶어줌
[Serializable]
public class PlayerGroundData
{
    //땅에 붙어있을 때 필요한 데이터들
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f; //회전 속도

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    //WalkSpeedModifier 값을 BaseSpeed에 곱해줄 거임
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
}

[Serializable]
public class PlayerAirData
{
    //공중에 떠있을 때 필요한 데이터들
    [field: Header("JumpData")]
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 5f;
}

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
}