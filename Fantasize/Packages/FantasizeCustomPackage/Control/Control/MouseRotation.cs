using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class MouseRotation : Controller
    {
        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
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
        //    lookAtDirection.y = 0f; // ĳ���ʹ� y�� ȸ������ �����Ƿ� y ��ǥ���� 0���� ����
        //    Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, iplayerInfo.GetRotationSpeed() * Time.deltaTime);
        //}
    }
}