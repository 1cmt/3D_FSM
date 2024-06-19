using System;
using System.Collections.Generic;
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

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; } //공격에 대한 정보 개수만큼 가져옴
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }
}

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; } //0번째 공격이면 1번째 공격 데이터를 불러와야 다음 콤보를 쓸 수 있음 고로 Index 1을 저장, 마지막은 다음이 없으니 -1저장
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; } //콤보는 특정 시간 안에 써야 나가는거임 그것을 판단
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; } //힘을 한번 실어 살짝 뒤로 밀리게끔 그게 가능한 시간인지
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; } 
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData GroundData { get; private set; }
    [field: SerializeField] public PlayerAirData AirData { get; private set; }
    [field: SerializeField] public PlayerAttackData AttackData { get; private set; }
}