using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class MouseRotation : Controller
    {
        // 0905 기획에서 마우스 회전 관련 삭제됨
        //protected override void Start()
        //{
        //    base.Start();
        //}

        //void Update()
        //{
        ////    Rotation();
        //}

        //private void Rotation()
        //{
        //    Vector3 mousePosition = Input.mousePosition;
        //    mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        //    Vector3 lookAtDirection = mousePosition - transform.position;
        //    lookAtDirection.y = 0f; // 캐릭터는 y축 회전하지 않으므로 y 좌표값은 0으로 설정
        //    Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, iplayerInfo.GetRotationSpeed() * Time.deltaTime);
        //}
    }
}