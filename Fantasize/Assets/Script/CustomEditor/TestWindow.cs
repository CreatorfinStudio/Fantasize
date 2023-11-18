using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Definition;
public class TestWindow : EditorWindow
{
    //TestWindowProperty t;

    //[MenuItem("Window/★틍원이 놀이터 o(^ 오^)o ★")]
    //public static void ShowWindow()
    //{
    //    GetWindow<TestWindow>("★틍원이 놀이터 o(^ 오^)o ★");
    //}

    //private void OnGUI()
    //{
    //    EditorGUILayout.Space(10);
    //    GUILayout.Label("== 에디터가 실행 중일때만 사용 가능 / 수정 원할 시 말하기 ==", EditorStyles.colorField);
    //    EditorGUILayout.Space(10);
    //    GUIStyle boldLabelStyle = new GUIStyle(EditorStyles.boldLabel);
    //    boldLabelStyle.normal.textColor = Color.yellow; // 원하는 색상으로 변경
    //    GUILayout.Label("[ 플 레 이 어 ] ", boldLabelStyle);
    //    EditorGUILayout.Space();

    //    GUILayout.Label("이동 속도", EditorStyles.boldLabel);
    //    t.MoveSpeed = EditorGUILayout.FloatField("Move Speed Value", t.MoveSpeed);
    //    GUILayout.Label("회전 속도", EditorStyles.boldLabel);
    //    t.RotationSpeed = EditorGUILayout.FloatField("Rotation Speed Value", t.RotationSpeed);
    //    GUILayout.Label("콤보 가능 횟수", EditorStyles.boldLabel);
    //    t.MaxComboAttacks = EditorGUILayout.IntField("Rotation Speed Value", t.MaxComboAttacks);

    //    EditorGUILayout.Space(10);

    //    if (GUILayout.Button("== [[ 클릭 -> 게임에 적용하기 ]] =="))
    //    {
    //        ApplyValuesToScript();
    //    }
    //}

    //// 본체에 적용~
    //private void ApplyValuesToScript()
    //{
    //    GameSingleton.Instance.Test_SetPlayerValue(t);
    //}
}
