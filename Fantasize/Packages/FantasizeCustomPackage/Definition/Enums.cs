using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    /// <summary>
    /// �÷��̾�
    /// </summary>
    public enum PlayerMove
    {
        None = -1,
        Idle,
        Walk,
        //Jump,
        WalkJump,
        BackWalk,
        Run,
        RunStop,
        RunJump,
        Sit,
        SitWalk,
      //  Attack, //�̰Ŵ� Move�ʺ��ٴ� �ٸ������� �����ҵ�. ����� Combo ��ũ��Ʈ���� �ִ�.
    }


    /// <summary>
    /// ���� ����
    /// </summary>
    public enum MonsterType
    {
        None = -1,
        Small,
        Medium,
        Boss,
    }

    /// <summary>
    /// ���� State
    /// </summary>
    public enum MonterState
    {
        None = -1,
        Roam,
        Attack,
        Die,
    }
}